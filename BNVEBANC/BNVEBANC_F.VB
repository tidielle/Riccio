Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEBANC_F

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

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    '---------------------------------
    'questa funzione riceve gli eventi dall'ENTITY: rimappata rispetto a quella standard di FRM__CHILD
    'prima eseguo quella standard
    If Not IsMyThrowRemoteEvent() Then Return 'il messaggio non Ã¨ per questa form ...
    MyBase.GestisciEventiEntity(sender, e)

    Try
      '---------------------------------
      'adesso gestisco le specifiche
      'devo inserire delle funzioni qui sotto per fare in modo che al variare di dati nell'entity delle informazioni 
      'legate all'interfaccia grafica (ui) vengano allineate a quanto richiesto dall'entity

      Select Case e.TipoEvento
        Case "AggiornaColoreAbiCab" : ColoraCampoAbiCab()
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Variabili"
  Public oCleBanc As CLEVEBANC
  Public dsBanc As DataSet
  Public dcBanc As BindingSource = New BindingSource
  Public oCallParams As CLE__CLDP
  Public dttDitta As New DataTable
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnBanc As NTSInformatica.NTSPanel
  Public WithEvents lbTb_codbanc As NTSInformatica.NTSLabel
  Public WithEvents edTb_codbanc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_desbanc As NTSInformatica.NTSLabel
  Public WithEvents edTb_desbanc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_conbanc As NTSInformatica.NTSLabel
  Public WithEvents edTb_conbanc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_abi As NTSInformatica.NTSLabel
  Public WithEvents edTb_abi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_cab As NTSInformatica.NTSLabel
  Public WithEvents edTb_cab As NTSInformatica.NTSTextBoxNum
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
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents edTb_csbf As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_rifriba As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_csbf As NTSInformatica.NTSLabel
  Public WithEvents lbTb_rifriba As NTSInformatica.NTSLabel
  Public WithEvents edTb_fido As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tiprap As NTSInformatica.NTSLabel
  Public WithEvents cbTb_tiprap As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_iban As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desbanc3 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_desbanc2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_iban As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desbanc3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_desbanc2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_ritfisc As NTSInformatica.NTSLabel
  Public WithEvents lbTb_fido As NTSInformatica.NTSLabel
  Public WithEvents edTb_ritfisc As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCambioDitta As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCondizioni As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbTassi As NTSInformatica.NTSBarButtonItem
  Public WithEvents fmIbanItalia As NTSInformatica.NTSGroupBox
  Public WithEvents edTb_cin As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_prefiban As NTSInformatica.NTSLabel
  Public WithEvents edTb_prefiban As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_conanticip As NTSInformatica.NTSLabel
  Public WithEvents edTb_conanticip As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_conanticip As NTSInformatica.NTSLabel
  Public WithEvents lbXx_conbanc As NTSInformatica.NTSLabel
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEBANC", "BEVEBANC", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128171550173281250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleBanc = CType(objTmp, CLEVEBANC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEBANC", strRemoteServer, strRemotePort)
    AddHandler oCleBanc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleBanc.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMVEBANC_F))
    Me.edTb_codbanc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_codbanc = New NTSInformatica.NTSLabel
    Me.edTb_desbanc = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_desbanc = New NTSInformatica.NTSLabel
    Me.lbTb_conbanc = New NTSInformatica.NTSLabel
    Me.edTb_conbanc = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cab = New NTSInformatica.NTSLabel
    Me.edTb_cab = New NTSInformatica.NTSTextBoxNum
    Me.edTb_abi = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_abi = New NTSInformatica.NTSLabel
    Me.lbTb_rifriba = New NTSInformatica.NTSLabel
    Me.lbTb_csbf = New NTSInformatica.NTSLabel
    Me.edTb_rifriba = New NTSInformatica.NTSTextBoxStr
    Me.edTb_csbf = New NTSInformatica.NTSTextBoxStr
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
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCambioDitta = New NTSInformatica.NTSBarButtonItem
    Me.tlbCondizioni = New NTSInformatica.NTSBarButtonItem
    Me.tlbTassi = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnBanc = New NTSInformatica.NTSPanel
    Me.lbXx_conanticip = New NTSInformatica.NTSLabel
    Me.lbXx_conbanc = New NTSInformatica.NTSLabel
    Me.lbTb_conanticip = New NTSInformatica.NTSLabel
    Me.edTb_conanticip = New NTSInformatica.NTSTextBoxNum
    Me.fmIbanItalia = New NTSInformatica.NTSGroupBox
    Me.lbTb_swift = New NTSInformatica.NTSLabel
    Me.edTb_swift = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_cin = New NTSInformatica.NTSLabel
    Me.edTb_cin = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_prefiban = New NTSInformatica.NTSLabel
    Me.edTb_prefiban = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_tiprap = New NTSInformatica.NTSLabel
    Me.cbTb_tiprap = New NTSInformatica.NTSComboBox
    Me.lbTb_iban = New NTSInformatica.NTSLabel
    Me.lbTb_desbanc3 = New NTSInformatica.NTSLabel
    Me.lbTb_desbanc2 = New NTSInformatica.NTSLabel
    Me.edTb_desbanc3 = New NTSInformatica.NTSTextBoxStr
    Me.edTb_iban = New NTSInformatica.NTSTextBoxStr
    Me.edTb_desbanc2 = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_ritfisc = New NTSInformatica.NTSLabel
    Me.lbTb_fido = New NTSInformatica.NTSLabel
    Me.edTb_ritfisc = New NTSInformatica.NTSTextBoxNum
    Me.edTb_fido = New NTSInformatica.NTSTextBoxNum
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_conbanc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_abi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_rifriba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_csbf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBanc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBanc.SuspendLayout()
    CType(Me.edTb_conanticip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIbanItalia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIbanItalia.SuspendLayout()
    CType(Me.edTb_swift.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_prefiban.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_tiprap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desbanc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_iban.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_desbanc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_ritfisc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_fido.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'edTb_codbanc
    '
    Me.edTb_codbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codbanc.EditValue = "0"
    Me.edTb_codbanc.Location = New System.Drawing.Point(181, 5)
    Me.edTb_codbanc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_codbanc.Name = "edTb_codbanc"
    Me.edTb_codbanc.NTSDbField = ""
    Me.edTb_codbanc.NTSFormat = "0"
    Me.edTb_codbanc.NTSForzaVisZoom = False
    Me.edTb_codbanc.NTSOldValue = ""
    Me.edTb_codbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codbanc.Properties.AutoHeight = False
    Me.edTb_codbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codbanc.Properties.MaxLength = 65536
    Me.edTb_codbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codbanc.Size = New System.Drawing.Size(57, 20)
    Me.edTb_codbanc.TabIndex = 0
    '
    'lbTb_codbanc
    '
    Me.lbTb_codbanc.AutoSize = True
    Me.lbTb_codbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codbanc.Location = New System.Drawing.Point(13, 8)
    Me.lbTb_codbanc.Name = "lbTb_codbanc"
    Me.lbTb_codbanc.NTSDbField = ""
    Me.lbTb_codbanc.Size = New System.Drawing.Size(39, 13)
    Me.lbTb_codbanc.TabIndex = 1
    Me.lbTb_codbanc.Text = "Codice"
    Me.lbTb_codbanc.Tooltip = ""
    Me.lbTb_codbanc.UseMnemonic = False
    '
    'edTb_desbanc
    '
    Me.edTb_desbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desbanc.Location = New System.Drawing.Point(181, 27)
    Me.edTb_desbanc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_desbanc.Name = "edTb_desbanc"
    Me.edTb_desbanc.NTSDbField = ""
    Me.edTb_desbanc.NTSForzaVisZoom = False
    Me.edTb_desbanc.NTSOldValue = ""
    Me.edTb_desbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_desbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desbanc.Properties.AutoHeight = False
    Me.edTb_desbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desbanc.Properties.MaxLength = 65536
    Me.edTb_desbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desbanc.Size = New System.Drawing.Size(274, 20)
    Me.edTb_desbanc.TabIndex = 2
    '
    'lbTb_desbanc
    '
    Me.lbTb_desbanc.AutoSize = True
    Me.lbTb_desbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desbanc.Location = New System.Drawing.Point(13, 30)
    Me.lbTb_desbanc.Name = "lbTb_desbanc"
    Me.lbTb_desbanc.NTSDbField = ""
    Me.lbTb_desbanc.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_desbanc.TabIndex = 3
    Me.lbTb_desbanc.Text = "Descrizione"
    Me.lbTb_desbanc.Tooltip = ""
    Me.lbTb_desbanc.UseMnemonic = False
    '
    'lbTb_conbanc
    '
    Me.lbTb_conbanc.AutoSize = True
    Me.lbTb_conbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conbanc.Location = New System.Drawing.Point(13, 52)
    Me.lbTb_conbanc.Name = "lbTb_conbanc"
    Me.lbTb_conbanc.NTSDbField = ""
    Me.lbTb_conbanc.Size = New System.Drawing.Size(110, 13)
    Me.lbTb_conbanc.TabIndex = 5
    Me.lbTb_conbanc.Text = "Conto Piano dei Conti"
    Me.lbTb_conbanc.Tooltip = ""
    Me.lbTb_conbanc.UseMnemonic = False
    '
    'edTb_conbanc
    '
    Me.edTb_conbanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conbanc.EditValue = "0"
    Me.edTb_conbanc.Location = New System.Drawing.Point(181, 49)
    Me.edTb_conbanc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_conbanc.Name = "edTb_conbanc"
    Me.edTb_conbanc.NTSDbField = ""
    Me.edTb_conbanc.NTSFormat = "0"
    Me.edTb_conbanc.NTSForzaVisZoom = False
    Me.edTb_conbanc.NTSOldValue = ""
    Me.edTb_conbanc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conbanc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conbanc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conbanc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conbanc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conbanc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conbanc.Properties.AutoHeight = False
    Me.edTb_conbanc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conbanc.Properties.MaxLength = 65536
    Me.edTb_conbanc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conbanc.Size = New System.Drawing.Size(100, 20)
    Me.edTb_conbanc.TabIndex = 1
    '
    'lbTb_cab
    '
    Me.lbTb_cab.AutoSize = True
    Me.lbTb_cab.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cab.Location = New System.Drawing.Point(5, 47)
    Me.lbTb_cab.Name = "lbTb_cab"
    Me.lbTb_cab.NTSDbField = ""
    Me.lbTb_cab.Size = New System.Drawing.Size(62, 13)
    Me.lbTb_cab.TabIndex = 7
    Me.lbTb_cab.Text = "Codice CAB"
    Me.lbTb_cab.Tooltip = ""
    Me.lbTb_cab.UseMnemonic = False
    '
    'edTb_cab
    '
    Me.edTb_cab.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cab.EditValue = "0"
    Me.edTb_cab.Location = New System.Drawing.Point(173, 44)
    Me.edTb_cab.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_cab.Name = "edTb_cab"
    Me.edTb_cab.NTSDbField = ""
    Me.edTb_cab.NTSFormat = "0"
    Me.edTb_cab.NTSForzaVisZoom = False
    Me.edTb_cab.NTSOldValue = ""
    Me.edTb_cab.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cab.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cab.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cab.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cab.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cab.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cab.Properties.AutoHeight = False
    Me.edTb_cab.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cab.Properties.MaxLength = 65536
    Me.edTb_cab.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cab.Size = New System.Drawing.Size(78, 20)
    Me.edTb_cab.TabIndex = 4
    '
    'edTb_abi
    '
    Me.edTb_abi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_abi.EditValue = "0"
    Me.edTb_abi.Location = New System.Drawing.Point(173, 22)
    Me.edTb_abi.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_abi.Name = "edTb_abi"
    Me.edTb_abi.NTSDbField = ""
    Me.edTb_abi.NTSFormat = "0"
    Me.edTb_abi.NTSForzaVisZoom = False
    Me.edTb_abi.NTSOldValue = ""
    Me.edTb_abi.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_abi.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_abi.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_abi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_abi.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_abi.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_abi.Properties.AutoHeight = False
    Me.edTb_abi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_abi.Properties.MaxLength = 65536
    Me.edTb_abi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_abi.Size = New System.Drawing.Size(78, 20)
    Me.edTb_abi.TabIndex = 3
    '
    'lbTb_abi
    '
    Me.lbTb_abi.AutoSize = True
    Me.lbTb_abi.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_abi.Location = New System.Drawing.Point(5, 25)
    Me.lbTb_abi.Name = "lbTb_abi"
    Me.lbTb_abi.NTSDbField = ""
    Me.lbTb_abi.Size = New System.Drawing.Size(59, 13)
    Me.lbTb_abi.TabIndex = 9
    Me.lbTb_abi.Text = "Codice ABI"
    Me.lbTb_abi.Tooltip = ""
    Me.lbTb_abi.UseMnemonic = False
    '
    'lbTb_rifriba
    '
    Me.lbTb_rifriba.AutoSize = True
    Me.lbTb_rifriba.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rifriba.Location = New System.Drawing.Point(361, 285)
    Me.lbTb_rifriba.Name = "lbTb_rifriba"
    Me.lbTb_rifriba.NTSDbField = ""
    Me.lbTb_rifriba.Size = New System.Drawing.Size(59, 13)
    Me.lbTb_rifriba.TabIndex = 10
    Me.lbTb_rifriba.Text = "Codice SIA"
    Me.lbTb_rifriba.Tooltip = ""
    Me.lbTb_rifriba.UseMnemonic = False
    '
    'lbTb_csbf
    '
    Me.lbTb_csbf.AutoSize = True
    Me.lbTb_csbf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_csbf.Location = New System.Drawing.Point(5, 69)
    Me.lbTb_csbf.Name = "lbTb_csbf"
    Me.lbTb_csbf.NTSDbField = ""
    Me.lbTb_csbf.Size = New System.Drawing.Size(40, 13)
    Me.lbTb_csbf.TabIndex = 11
    Me.lbTb_csbf.Text = "N° C/C"
    Me.lbTb_csbf.Tooltip = ""
    Me.lbTb_csbf.UseMnemonic = False
    '
    'edTb_rifriba
    '
    Me.edTb_rifriba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_rifriba.Location = New System.Drawing.Point(505, 282)
    Me.edTb_rifriba.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_rifriba.Name = "edTb_rifriba"
    Me.edTb_rifriba.NTSDbField = ""
    Me.edTb_rifriba.NTSForzaVisZoom = False
    Me.edTb_rifriba.NTSOldValue = ""
    Me.edTb_rifriba.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_rifriba.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_rifriba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_rifriba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_rifriba.Properties.AutoHeight = False
    Me.edTb_rifriba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_rifriba.Properties.MaxLength = 65536
    Me.edTb_rifriba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_rifriba.Size = New System.Drawing.Size(100, 20)
    Me.edTb_rifriba.TabIndex = 12
    '
    'edTb_csbf
    '
    Me.edTb_csbf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_csbf.Location = New System.Drawing.Point(173, 66)
    Me.edTb_csbf.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_csbf.Name = "edTb_csbf"
    Me.edTb_csbf.NTSDbField = ""
    Me.edTb_csbf.NTSForzaVisZoom = False
    Me.edTb_csbf.NTSOldValue = ""
    Me.edTb_csbf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_csbf.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_csbf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_csbf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_csbf.Properties.AutoHeight = False
    Me.edTb_csbf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_csbf.Properties.MaxLength = 65536
    Me.edTb_csbf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_csbf.Size = New System.Drawing.Size(274, 20)
    Me.edTb_csbf.TabIndex = 13
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbCambioDitta, Me.tlbCondizioni, Me.tlbTassi})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
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
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
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
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 8
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 9
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCondizioni, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTassi), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "Cambio ditta"
    Me.tlbCambioDitta.Id = 15
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.Visible = True
    '
    'tlbCondizioni
    '
    Me.tlbCondizioni.Caption = "Condizioni per tipo di operazione"
    Me.tlbCondizioni.Id = 16
    Me.tlbCondizioni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbCondizioni.Name = "tlbCondizioni"
    Me.tlbCondizioni.Visible = True
    '
    'tlbTassi
    '
    Me.tlbTassi.Caption = "Tassi applicati"
    Me.tlbTassi.Id = 17
    Me.tlbTassi.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F8))
    Me.tlbTassi.Name = "tlbTassi"
    Me.tlbTassi.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 10
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 11
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 12
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 13
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 14
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnBanc
    '
    Me.pnBanc.AllowDrop = True
    Me.pnBanc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBanc.Appearance.Options.UseBackColor = True
    Me.pnBanc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBanc.Controls.Add(Me.lbXx_conanticip)
    Me.pnBanc.Controls.Add(Me.lbXx_conbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_conanticip)
    Me.pnBanc.Controls.Add(Me.edTb_conanticip)
    Me.pnBanc.Controls.Add(Me.fmIbanItalia)
    Me.pnBanc.Controls.Add(Me.lbTb_tiprap)
    Me.pnBanc.Controls.Add(Me.cbTb_tiprap)
    Me.pnBanc.Controls.Add(Me.lbTb_iban)
    Me.pnBanc.Controls.Add(Me.lbTb_desbanc3)
    Me.pnBanc.Controls.Add(Me.lbTb_desbanc2)
    Me.pnBanc.Controls.Add(Me.edTb_desbanc3)
    Me.pnBanc.Controls.Add(Me.edTb_iban)
    Me.pnBanc.Controls.Add(Me.edTb_desbanc2)
    Me.pnBanc.Controls.Add(Me.lbTb_ritfisc)
    Me.pnBanc.Controls.Add(Me.lbTb_fido)
    Me.pnBanc.Controls.Add(Me.edTb_ritfisc)
    Me.pnBanc.Controls.Add(Me.edTb_fido)
    Me.pnBanc.Controls.Add(Me.edTb_rifriba)
    Me.pnBanc.Controls.Add(Me.lbTb_rifriba)
    Me.pnBanc.Controls.Add(Me.edTb_codbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_codbanc)
    Me.pnBanc.Controls.Add(Me.edTb_desbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_desbanc)
    Me.pnBanc.Controls.Add(Me.edTb_conbanc)
    Me.pnBanc.Controls.Add(Me.lbTb_conbanc)
    Me.pnBanc.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBanc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnBanc.Location = New System.Drawing.Point(0, 30)
    Me.pnBanc.Name = "pnBanc"
    Me.pnBanc.NTSActiveTrasparency = True
    Me.pnBanc.Size = New System.Drawing.Size(643, 352)
    Me.pnBanc.TabIndex = 10
    Me.pnBanc.Text = "pnBanc"
    '
    'lbXx_conanticip
    '
    Me.lbXx_conanticip.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conanticip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conanticip.Location = New System.Drawing.Point(287, 71)
    Me.lbXx_conanticip.Name = "lbXx_conanticip"
    Me.lbXx_conanticip.NTSDbField = ""
    Me.lbXx_conanticip.Size = New System.Drawing.Size(347, 20)
    Me.lbXx_conanticip.TabIndex = 37
    Me.lbXx_conanticip.Tooltip = ""
    Me.lbXx_conanticip.UseMnemonic = False
    '
    'lbXx_conbanc
    '
    Me.lbXx_conbanc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conbanc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conbanc.Location = New System.Drawing.Point(287, 49)
    Me.lbXx_conbanc.Name = "lbXx_conbanc"
    Me.lbXx_conbanc.NTSDbField = ""
    Me.lbXx_conbanc.Size = New System.Drawing.Size(347, 20)
    Me.lbXx_conbanc.TabIndex = 36
    Me.lbXx_conbanc.Tooltip = ""
    Me.lbXx_conbanc.UseMnemonic = False
    '
    'lbTb_conanticip
    '
    Me.lbTb_conanticip.AutoSize = True
    Me.lbTb_conanticip.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_conanticip.Location = New System.Drawing.Point(13, 74)
    Me.lbTb_conanticip.Name = "lbTb_conanticip"
    Me.lbTb_conanticip.NTSDbField = ""
    Me.lbTb_conanticip.Size = New System.Drawing.Size(151, 13)
    Me.lbTb_conanticip.TabIndex = 34
    Me.lbTb_conanticip.Text = "Conto per anticipazioni su C/C"
    Me.lbTb_conanticip.Tooltip = ""
    Me.lbTb_conanticip.UseMnemonic = False
    '
    'edTb_conanticip
    '
    Me.edTb_conanticip.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_conanticip.EditValue = "0"
    Me.edTb_conanticip.Location = New System.Drawing.Point(181, 71)
    Me.edTb_conanticip.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_conanticip.Name = "edTb_conanticip"
    Me.edTb_conanticip.NTSDbField = ""
    Me.edTb_conanticip.NTSFormat = "0"
    Me.edTb_conanticip.NTSForzaVisZoom = False
    Me.edTb_conanticip.NTSOldValue = ""
    Me.edTb_conanticip.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_conanticip.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_conanticip.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_conanticip.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_conanticip.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_conanticip.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_conanticip.Properties.AutoHeight = False
    Me.edTb_conanticip.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_conanticip.Properties.MaxLength = 65536
    Me.edTb_conanticip.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_conanticip.Size = New System.Drawing.Size(100, 20)
    Me.edTb_conanticip.TabIndex = 33
    '
    'fmIbanItalia
    '
    Me.fmIbanItalia.AllowDrop = True
    Me.fmIbanItalia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIbanItalia.Appearance.Options.UseBackColor = True
    Me.fmIbanItalia.Controls.Add(Me.lbTb_swift)
    Me.fmIbanItalia.Controls.Add(Me.edTb_swift)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_cin)
    Me.fmIbanItalia.Controls.Add(Me.edTb_cin)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_prefiban)
    Me.fmIbanItalia.Controls.Add(Me.edTb_prefiban)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_csbf)
    Me.fmIbanItalia.Controls.Add(Me.edTb_cab)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_cab)
    Me.fmIbanItalia.Controls.Add(Me.edTb_abi)
    Me.fmIbanItalia.Controls.Add(Me.lbTb_abi)
    Me.fmIbanItalia.Controls.Add(Me.edTb_csbf)
    Me.fmIbanItalia.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIbanItalia.Location = New System.Drawing.Point(8, 93)
    Me.fmIbanItalia.Name = "fmIbanItalia"
    Me.fmIbanItalia.Size = New System.Drawing.Size(626, 140)
    Me.fmIbanItalia.TabIndex = 31
    Me.fmIbanItalia.Text = "IBAN italia"
    '
    'lbTb_swift
    '
    Me.lbTb_swift.AutoSize = True
    Me.lbTb_swift.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_swift.Location = New System.Drawing.Point(6, 114)
    Me.lbTb_swift.Name = "lbTb_swift"
    Me.lbTb_swift.NTSDbField = ""
    Me.lbTb_swift.Size = New System.Drawing.Size(48, 13)
    Me.lbTb_swift.TabIndex = 672
    Me.lbTb_swift.Text = "Bic/Swift"
    Me.lbTb_swift.Tooltip = ""
    Me.lbTb_swift.UseMnemonic = False
    '
    'edTb_swift
    '
    Me.edTb_swift.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTb_swift.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_swift.Location = New System.Drawing.Point(173, 111)
    Me.edTb_swift.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_swift.Name = "edTb_swift"
    Me.edTb_swift.NTSDbField = ""
    Me.edTb_swift.NTSForzaVisZoom = False
    Me.edTb_swift.NTSOldValue = ""
    Me.edTb_swift.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_swift.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_swift.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_swift.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_swift.Properties.AutoHeight = False
    Me.edTb_swift.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_swift.Properties.MaxLength = 65536
    Me.edTb_swift.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_swift.Size = New System.Drawing.Size(274, 20)
    Me.edTb_swift.TabIndex = 671
    '
    'lbTb_cin
    '
    Me.lbTb_cin.AutoSize = True
    Me.lbTb_cin.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cin.Location = New System.Drawing.Point(466, 69)
    Me.lbTb_cin.Name = "lbTb_cin"
    Me.lbTb_cin.NTSDbField = ""
    Me.lbTb_cin.Size = New System.Drawing.Size(25, 13)
    Me.lbTb_cin.TabIndex = 22
    Me.lbTb_cin.Text = "CIN"
    Me.lbTb_cin.Tooltip = ""
    Me.lbTb_cin.UseMnemonic = False
    '
    'edTb_cin
    '
    Me.edTb_cin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cin.Location = New System.Drawing.Point(497, 66)
    Me.edTb_cin.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_cin.Name = "edTb_cin"
    Me.edTb_cin.NTSDbField = ""
    Me.edTb_cin.NTSForzaVisZoom = False
    Me.edTb_cin.NTSOldValue = ""
    Me.edTb_cin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_cin.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_cin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cin.Properties.AutoHeight = False
    Me.edTb_cin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cin.Properties.MaxLength = 65536
    Me.edTb_cin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cin.Size = New System.Drawing.Size(24, 20)
    Me.edTb_cin.TabIndex = 21
    '
    'lbTb_prefiban
    '
    Me.lbTb_prefiban.AutoSize = True
    Me.lbTb_prefiban.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_prefiban.Location = New System.Drawing.Point(5, 91)
    Me.lbTb_prefiban.Name = "lbTb_prefiban"
    Me.lbTb_prefiban.NTSDbField = ""
    Me.lbTb_prefiban.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_prefiban.TabIndex = 20
    Me.lbTb_prefiban.Text = "Prefisso IBAN"
    Me.lbTb_prefiban.Tooltip = ""
    Me.lbTb_prefiban.UseMnemonic = False
    '
    'edTb_prefiban
    '
    Me.edTb_prefiban.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_prefiban.Location = New System.Drawing.Point(173, 88)
    Me.edTb_prefiban.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_prefiban.Name = "edTb_prefiban"
    Me.edTb_prefiban.NTSDbField = ""
    Me.edTb_prefiban.NTSForzaVisZoom = False
    Me.edTb_prefiban.NTSOldValue = ""
    Me.edTb_prefiban.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_prefiban.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_prefiban.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_prefiban.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_prefiban.Properties.AutoHeight = False
    Me.edTb_prefiban.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_prefiban.Properties.MaxLength = 65536
    Me.edTb_prefiban.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_prefiban.Size = New System.Drawing.Size(100, 20)
    Me.edTb_prefiban.TabIndex = 19
    '
    'lbTb_tiprap
    '
    Me.lbTb_tiprap.AutoSize = True
    Me.lbTb_tiprap.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_tiprap.Location = New System.Drawing.Point(14, 285)
    Me.lbTb_tiprap.Name = "lbTb_tiprap"
    Me.lbTb_tiprap.NTSDbField = ""
    Me.lbTb_tiprap.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_tiprap.TabIndex = 25
    Me.lbTb_tiprap.Text = "Tipo rapporto"
    Me.lbTb_tiprap.Tooltip = ""
    Me.lbTb_tiprap.UseMnemonic = False
    '
    'cbTb_tiprap
    '
    Me.cbTb_tiprap.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_tiprap.DataSource = Nothing
    Me.cbTb_tiprap.DisplayMember = ""
    Me.cbTb_tiprap.Location = New System.Drawing.Point(181, 282)
    Me.cbTb_tiprap.Name = "cbTb_tiprap"
    Me.cbTb_tiprap.NTSDbField = ""
    Me.cbTb_tiprap.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_tiprap.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_tiprap.Properties.AutoHeight = False
    Me.cbTb_tiprap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_tiprap.Properties.DropDownRows = 30
    Me.cbTb_tiprap.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_tiprap.SelectedValue = ""
    Me.cbTb_tiprap.Size = New System.Drawing.Size(100, 20)
    Me.cbTb_tiprap.TabIndex = 24
    Me.cbTb_tiprap.ValueMember = ""
    '
    'lbTb_iban
    '
    Me.lbTb_iban.AutoSize = True
    Me.lbTb_iban.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_iban.Location = New System.Drawing.Point(13, 241)
    Me.lbTb_iban.Name = "lbTb_iban"
    Me.lbTb_iban.NTSDbField = ""
    Me.lbTb_iban.Size = New System.Drawing.Size(65, 13)
    Me.lbTb_iban.TabIndex = 23
    Me.lbTb_iban.Text = "IBAN estero"
    Me.lbTb_iban.Tooltip = ""
    Me.lbTb_iban.UseMnemonic = False
    '
    'lbTb_desbanc3
    '
    Me.lbTb_desbanc3.AutoSize = True
    Me.lbTb_desbanc3.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desbanc3.Location = New System.Drawing.Point(14, 329)
    Me.lbTb_desbanc3.Name = "lbTb_desbanc3"
    Me.lbTb_desbanc3.NTSDbField = ""
    Me.lbTb_desbanc3.Size = New System.Drawing.Size(123, 13)
    Me.lbTb_desbanc3.TabIndex = 22
    Me.lbTb_desbanc3.Text = "Descrizione aggiuntiva 2"
    Me.lbTb_desbanc3.Tooltip = ""
    Me.lbTb_desbanc3.UseMnemonic = False
    '
    'lbTb_desbanc2
    '
    Me.lbTb_desbanc2.AutoSize = True
    Me.lbTb_desbanc2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_desbanc2.Location = New System.Drawing.Point(14, 307)
    Me.lbTb_desbanc2.Name = "lbTb_desbanc2"
    Me.lbTb_desbanc2.NTSDbField = ""
    Me.lbTb_desbanc2.Size = New System.Drawing.Size(123, 13)
    Me.lbTb_desbanc2.TabIndex = 21
    Me.lbTb_desbanc2.Text = "Descrizione aggiuntiva 1"
    Me.lbTb_desbanc2.Tooltip = ""
    Me.lbTb_desbanc2.UseMnemonic = False
    '
    'edTb_desbanc3
    '
    Me.edTb_desbanc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desbanc3.Location = New System.Drawing.Point(181, 326)
    Me.edTb_desbanc3.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_desbanc3.Name = "edTb_desbanc3"
    Me.edTb_desbanc3.NTSDbField = ""
    Me.edTb_desbanc3.NTSForzaVisZoom = False
    Me.edTb_desbanc3.NTSOldValue = ""
    Me.edTb_desbanc3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_desbanc3.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desbanc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desbanc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desbanc3.Properties.AutoHeight = False
    Me.edTb_desbanc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desbanc3.Properties.MaxLength = 65536
    Me.edTb_desbanc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desbanc3.Size = New System.Drawing.Size(453, 20)
    Me.edTb_desbanc3.TabIndex = 19
    '
    'edTb_iban
    '
    Me.edTb_iban.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTb_iban.Location = New System.Drawing.Point(181, 238)
    Me.edTb_iban.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_iban.Name = "edTb_iban"
    Me.edTb_iban.NTSDbField = ""
    Me.edTb_iban.NTSForzaVisZoom = False
    Me.edTb_iban.NTSOldValue = ""
    Me.edTb_iban.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_iban.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_iban.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_iban.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_iban.Properties.AutoHeight = False
    Me.edTb_iban.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_iban.Properties.MaxLength = 65536
    Me.edTb_iban.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_iban.Size = New System.Drawing.Size(453, 20)
    Me.edTb_iban.TabIndex = 20
    '
    'edTb_desbanc2
    '
    Me.edTb_desbanc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_desbanc2.Location = New System.Drawing.Point(181, 304)
    Me.edTb_desbanc2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_desbanc2.Name = "edTb_desbanc2"
    Me.edTb_desbanc2.NTSDbField = ""
    Me.edTb_desbanc2.NTSForzaVisZoom = False
    Me.edTb_desbanc2.NTSOldValue = ""
    Me.edTb_desbanc2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_desbanc2.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_desbanc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_desbanc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_desbanc2.Properties.AutoHeight = False
    Me.edTb_desbanc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_desbanc2.Properties.MaxLength = 65536
    Me.edTb_desbanc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_desbanc2.Size = New System.Drawing.Size(453, 20)
    Me.edTb_desbanc2.TabIndex = 18
    '
    'lbTb_ritfisc
    '
    Me.lbTb_ritfisc.AutoSize = True
    Me.lbTb_ritfisc.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_ritfisc.Location = New System.Drawing.Point(361, 263)
    Me.lbTb_ritfisc.Name = "lbTb_ritfisc"
    Me.lbTb_ritfisc.NTSDbField = ""
    Me.lbTb_ritfisc.Size = New System.Drawing.Size(138, 13)
    Me.lbTb_ritfisc.TabIndex = 17
    Me.lbTb_ritfisc.Text = "Percentuale ritenuta fiscale"
    Me.lbTb_ritfisc.Tooltip = ""
    Me.lbTb_ritfisc.UseMnemonic = False
    '
    'lbTb_fido
    '
    Me.lbTb_fido.AutoSize = True
    Me.lbTb_fido.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_fido.Location = New System.Drawing.Point(13, 263)
    Me.lbTb_fido.Name = "lbTb_fido"
    Me.lbTb_fido.NTSDbField = ""
    Me.lbTb_fido.Size = New System.Drawing.Size(66, 13)
    Me.lbTb_fido.TabIndex = 16
    Me.lbTb_fido.Text = "Importo fido"
    Me.lbTb_fido.Tooltip = ""
    Me.lbTb_fido.UseMnemonic = False
    '
    'edTb_ritfisc
    '
    Me.edTb_ritfisc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_ritfisc.EditValue = "0"
    Me.edTb_ritfisc.Location = New System.Drawing.Point(505, 260)
    Me.edTb_ritfisc.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_ritfisc.Name = "edTb_ritfisc"
    Me.edTb_ritfisc.NTSDbField = ""
    Me.edTb_ritfisc.NTSFormat = "0"
    Me.edTb_ritfisc.NTSForzaVisZoom = False
    Me.edTb_ritfisc.NTSOldValue = ""
    Me.edTb_ritfisc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_ritfisc.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_ritfisc.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_ritfisc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_ritfisc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_ritfisc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_ritfisc.Properties.AutoHeight = False
    Me.edTb_ritfisc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_ritfisc.Properties.MaxLength = 65536
    Me.edTb_ritfisc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_ritfisc.Size = New System.Drawing.Size(56, 20)
    Me.edTb_ritfisc.TabIndex = 15
    '
    'edTb_fido
    '
    Me.edTb_fido.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_fido.EditValue = "0"
    Me.edTb_fido.Location = New System.Drawing.Point(181, 260)
    Me.edTb_fido.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.edTb_fido.Name = "edTb_fido"
    Me.edTb_fido.NTSDbField = ""
    Me.edTb_fido.NTSFormat = "0"
    Me.edTb_fido.NTSForzaVisZoom = False
    Me.edTb_fido.NTSOldValue = ""
    Me.edTb_fido.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_fido.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_fido.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_fido.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_fido.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_fido.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_fido.Properties.AutoHeight = False
    Me.edTb_fido.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_fido.Properties.MaxLength = 65536
    Me.edTb_fido.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_fido.Size = New System.Drawing.Size(100, 20)
    Me.edTb_fido.TabIndex = 14
    '
    'FRMVEBANC_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(643, 382)
    Me.Controls.Add(Me.pnBanc)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMVEBANC_F"
    Me.Text = "BANCHE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_conbanc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_abi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_rifriba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_csbf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBanc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBanc.ResumeLayout(False)
    Me.pnBanc.PerformLayout()
    CType(Me.edTb_conanticip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIbanItalia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIbanItalia.ResumeLayout(False)
    Me.fmIbanItalia.PerformLayout()
    CType(Me.edTb_swift.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_prefiban.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_tiprap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desbanc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_iban.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_desbanc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_ritfisc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_fido.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRMVEBANC_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcBanc.Dispose()
      dsBanc.Dispose()
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Sub FRMVEBANC_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEBANC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'Carico i combobox
      CaricaCombo()

      'Predispongo i controlli
      InitControls()

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMVEBANC_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Try
      LeggiDatiDitta()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If dsBanc.Tables("TABBANC").Rows.Count = 1 And dsBanc.Tables("TABBANC").Rows(0).RowState = DataRowState.Added Then
            'sono già in record nuovo perchè la tabella è vuota: non devo fare nulla
          Else
            tlbNuovo_ItemClick(Me, Nothing)
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i = 0 To dcBanc.List.Count - 1
            If CType(dcBanc.Item(i), DataRowView)!tb_codbanc.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcBanc.Position = i
              Exit For
            End If
          Next
        End If
      End If    'If Not oCallParams Is Nothing Then
      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Sub CaricaCombo()
    Dim dttTiprap As New DataTable()
    Try
      dttTiprap.Columns.Add("cod", GetType(String))
      dttTiprap.Columns.Add("val", GetType(String))
      dttTiprap.Rows.Add(New Object() {"B", "C/C"})
      dttTiprap.Rows.Add(New Object() {"C", "C/SBF"})
      dttTiprap.Rows.Add(New Object() {"D", "Anticipi Estero"})
      dttTiprap.Rows.Add(New Object() {"E", "Anticipi Italia"})
      dttTiprap.Rows.Add(New Object() {"Z", "C/Antic. da SBF"})
      dttTiprap.AcceptChanges()
      cbTb_tiprap.DataSource = dttTiprap
      cbTb_tiprap.ValueMember = "cod"
      cbTb_tiprap.DisplayMember = "val"

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
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'Non gestisco l'errore: 
        'se non c'è una immagine prendo quella a design
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------------------
      'Completo le informazioni dei controlli
      edTb_codbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322337308, "Cod. Banca"), tabbanc)
      edTb_desbanc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023322493481, "Descr. Banca"), 30, True)
      edTb_conbanc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322649654, "Cod. Conto P.d.C."), tabanagras)
      edTb_abi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322805827, "Cod. ABI"), tababi)
      edTb_cab.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023322962000, "Cod. CAB"), tababicab)
      edTb_rifriba.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323118173, "Cod. SIA"), 12, True)
      edTb_csbf.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323274346, "N° Conto"), 12, True)
      edTb_fido.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323430519, "Importo Fido"), oApp.FormatImporti, 20, 0, 9999999999999)
      edTb_ritfisc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323586692, "% Ritenuta Fiscale"), oApp.FormatSconti, 6, 0, 100)
      cbTb_tiprap.NTSSetParam(oApp.Tr(Me, 128230023323742865, "Tipo di rapporto"))
      edTb_desbanc2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023323899038, "Descr. agg. 1"), 30, True)
      edTb_desbanc3.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324055211, "Descr. agg. 2"), 30, True)
      edTb_iban.NTSSetParam(oMenu, oApp.Tr(Me, 128230023324211384, "Cod. IBAN"), 50, True)
      edTb_cin.NTSSetParam(oMenu, oApp.Tr(Me, 128378861751612000, "Cin"), 1, True)
      edTb_prefiban.NTSSetParam(oMenu, oApp.Tr(Me, 128378861751924000, "Prefisso IBAN"), 4, True)
      edTb_conanticip.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129555559109316406, "Conto x giroconto anticipazioni su C/C"), tabanagras)
      edTb_swift.NTSSetParam(oMenu, oApp.Tr(Me, 130187110440982855, "Codice BIC/SWIFT"), 14, True)

      edTb_codbanc.NTSForzaVisZoom = False 'Non permette lo ZOOM
      edTb_desbanc.NTSForzaVisZoom = True
      edTb_conbanc.NTSForzaVisZoom = True
      edTb_abi.NTSForzaVisZoom = True
      edTb_cab.NTSForzaVisZoom = True

      edTb_desbanc.NTSSetParamZoom("")

      edTb_codbanc.NTSSetRichiesto()

      '---------------------------------------------------------------------------
      'Chiamo lo script per inizializzare i controlli caricati con Source Extender
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
      'Se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'Collego il BindingSource ai vari controlli 
      edTb_codbanc.NTSDbField = "tabbanc.tb_codbanc"
      edTb_desbanc.NTSDbField = "tabbanc.tb_desbanc"
      edTb_conbanc.NTSDbField = "tabbanc.tb_conbanc"
      edTb_abi.NTSDbField = "tabbanc.tb_abi"
      edTb_cab.NTSDbField = "tabbanc.tb_cab"
      edTb_rifriba.NTSDbField = "tabbanc.tb_rifriba"
      edTb_swift.NTSDbField = "tabbanc.tb_swift"
      edTb_csbf.NTSDbField = "tabbanc.tb_csbf"
      edTb_fido.NTSDbField = "tabbanc.tb_fido"
      edTb_ritfisc.NTSDbField = "tabbanc.tb_ritfisc"
      cbTb_tiprap.NTSDbField = "tabbanc.tb_tiprap"
      edTb_desbanc2.NTSDbField = "tabbanc.tb_desbanc2"
      edTb_desbanc3.NTSDbField = "tabbanc.tb_desbanc3"
      edTb_iban.NTSDbField = "tabbanc.tb_iban"
      edTb_cin.NTSDbField = "tabbanc.tb_cin"
      edTb_prefiban.NTSDbField = "tabbanc.tb_prefiban"
      edTb_conanticip.NTSDbField = "tabbanc.tb_conanticip"
      lbXx_conbanc.NTSDbField = "tabbanc.xx_conbanc"
      lbXx_conanticip.NTSDbField = "tabbanc.xx_conanticip"
      '-------------------------------------------------
      'Per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcBanc, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub LeggiDatiDitta()
    Dim bAnagen As Boolean = False
    Dim bDbMultiDitta As Boolean = False
    Try
      '-------------------------------------------------
      'se ci sono le caratteristiche visualizzo lo zoom per selezionare le ditte
      'se oCallParams.strOpen <> "" non visualizzera lo zoom ditte e prednerà la ditta passata da lchild padre
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BNVEBANC", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      If DittaCorrente = "" Then Me.Close()

      If bAnagen = False Or bDbMultiDitta = False Then
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If

      '-------------------------------------------------
      'Leggo le informazioni relative alla ditta corrente e le passo all'ENTITY
      oCleBanc.strDittaCorrente = DittaCorrente
      oCleBanc.bModCG = CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModCG)            'Memorizzo nell'entity la presenza del modulo di CG
      oCleBanc.bModExtCGE = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCGE) 'Memorizzo nell'entity la presenza del modulo di CGE

      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)

      '-------------------------------------------------
      'Leggo dal database i dati e collego l'NTSBinding
      If Not oCleBanc.Apri(DittaCorrente, dsBanc) Then Me.Close()
      dcBanc.DataSource = dsBanc.Tables("TABBANC")
      dsBanc.AcceptChanges()
      If dsBanc.Tables("TABBANC").Rows.Count > 0 Then
        edTb_codbanc.Enabled = False
      End If

      '-------------------------------------------------
      'Collego il BindingSource ai vari controlli 
      Bindcontrols()
      '-------------------------------------------------

      'Refresh dei controlli
      If dsBanc.Tables("TABBANC").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcBanc.ResetBindings(False) 'False = non è cambiato il datatable sottostante (da noi è sempre così)
      dcBanc.MoveFirst()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'Chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

      If oCleBanc.RecordIsChanged Then

        'Controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550313281250, "Salvare le modifiche apportate alla Banca?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleBanc.Salva(False) Then Return False
          If dsBanc.Tables("TABBANC").Rows.Count > 0 Then
            edTb_codbanc.Enabled = False
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

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'Preparo il motore di stampa
      strCrpe = "{tabbanc.codditt} = " & CStrSQL(DittaCorrente)
      nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSVEBANC", "Reports1", " ", 0, nDestin, "BSVEBANC.RPT", False, "Banche", False)
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

#Region "Toolbar"

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'Vado sul primo record
      If Not Salva() Then Return
      dcBanc.MoveFirst()
      ColoraCampoAbiCab()
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
      dcBanc.MovePrevious()
      ColoraCampoAbiCab()
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
      dcBanc.MoveNext()
      ColoraCampoAbiCab()
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
      dcBanc.MoveLast()
      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'Creo una nuova Banca
      If Not Salva() Then Return
      oCleBanc.Nuovo()
      dcBanc.MoveLast()
      edTb_codbanc.Enabled = True
      edTb_codbanc.Focus()

      '-------------------------------------------------
      'Imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()
      ColoraCampoAbiCab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try

      Salva()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleBanc.dsShared.Tables("TABBANC").Rows(dcBanc.Position).RowState <> DataRowState.Added Then
        If oCleBanc.TestPreCancella(NTSCInt(edTb_codbanc.Text), strMsg) = False Then
          oApp.MsgBoxInfo(strMsg)
          Return
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'Cancello la Banca
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550430781250, "Cancellare la Banca?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsBanc.Tables("TABBANC").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcBanc.RemoveAt(dcBanc.Position)
          oCleBanc.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcBanc, Me)
            bRemovBinding = False
            edTb_codbanc.Enabled = True
          Else
            edTb_codbanc.Enabled = False
          End If
          Return
      End Select
      ColoraCampoAbiCab()
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcBanc, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'Ripristino la Banca
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128171550517812500, "Ripristinare le modifiche apportate alla Banca?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsBanc.Tables("TABBANC").Rows.Count = 1 And dsBanc.Tables("TABBANC").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleBanc.Ripristina(dcBanc.Position, dcBanc.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcBanc, Me)
            bRemovBinding = False
            edTb_codbanc.Enabled = True
          Else
            edTb_codbanc.Enabled = False
          End If
      End Select
      ColoraCampoAbiCab()
    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcBanc, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim i As Integer
    Dim oParam As New CLE__PATB
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    Try
      'SendKeys.SendWait("{F5}")
      'se faccio la riga sopra va in un loop infinito....
      'devo vedere quale controllo ha il focus, quindi lanciare lo zoom direttamente sul controllo, senza forzare l'F5
      If edTb_desbanc.Focused Then

        Me.ValidaLastControl()

        'Lancio lo zoom solo per riposizionare il BindingSource
        If oCleBanc.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128777131353619988, "Salvare o Ripristinare le modifiche apportate alla Banca prima di selezionarne una nuova."))
          Return
        End If

        NTSZOOM.strIn = edTb_codbanc.Text
        NTSZOOM.ZoomStrIn("ZOOMTABBANC", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edTb_codbanc.Text Then
          'Mi sposto sul record selezionato
          For i = 0 To dcBanc.List.Count - 1
            If CType(dcBanc.Item(i), DataRowView)!tb_codbanc.ToString = NTSZOOM.strIn Then
              dcBanc.Position = i
              Exit For
            End If
          Next
        End If
      ElseIf edTb_conbanc.Focused Then
        '--------------------------------------------
        'Zoom ANAGRAS
        SetFastZoom(edTb_conbanc.Text, oParam)    'abilito la gestione dello zoom veloce
        'oParam.bVisGriglia = True               'non serve è il default
        'oParam.strTipo = "S"                    'non serve, è scontato
        oParam.bTipoProposto = False
        NTSZOOM.strIn = edTb_conbanc.Text
        NTSZOOM.ZoomStrIn("ZOOMANAGRAS", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_conbanc.Text Then
          edTb_conbanc.NTSTextDB = NTSZOOM.strIn
        End If
      ElseIf edTb_cab.Focused Then
        'Zoom CAB
        SetFastZoom(edTb_cab.Text, oParam)    'Abilito la gestione dello ZOOM veloce
        oParam.strDescr = edTb_abi.Text       'Passo il codice ABI
        NTSZOOM.strIn = ""
        NTSZOOM.ZoomStrIn("ZOOMABICAB", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_cab.Text Then
          edTb_cab.NTSTextDB = NTSZOOM.strIn
        End If
      Else
        'Per eventuali altri controlli caricati al volo
        'Zoom standard di Textbox e Griglia
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
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
      If Not Salva() Then Return
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Try
      If Not Salva() Then Return
      LeggiDatiDitta()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCondizioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCondizioni.ItemClick
    Dim frmCoba As FRMTECOBA = Nothing

    Try
      If Not Salva() Then Return

      oCleBanc.lCodBanc = NTSCInt(edTb_codbanc.Text)

      frmCoba = CType(NTSNewFormModal("FRMTECOBA"), FRMTECOBA)
      If Not frmCoba.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmCoba.InitEntity(oCleBanc)
      frmCoba.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCoba Is Nothing Then frmCoba.Dispose()
      frmCoba = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbTassi_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTassi.ItemClick
    Dim frmTass As FRMTETASS = Nothing

    Try
      If Not Salva() Then Return

      oCleBanc.lCodBanc = NTSCInt(edTb_codbanc.Text)

      frmTass = CType(NTSNewFormModal("FRMTETASS"), FRMTETASS)
      If Not frmTass.Init(oMenu, Nothing, DittaCorrente, Nothing) Then Return
      frmTass.InitEntity(oCleBanc)
      frmTass.ShowDialog(Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmTass Is Nothing Then frmTass.Dispose()
      frmTass = Nothing
    End Try
  End Sub

#End Region

  Public Overridable Sub ColoraCampoAbiCab()
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(edTb_abi.Text) = 0 Then
        'Abi non settato, non uso il colore di avviso
        edTb_abi.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(edTb_abi.Text, DittaCorrente, "ABI", "N", , dttTmp)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abiabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edTb_abi.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edTb_abi.BackColor = Color.Transparent
        End If
      End If

      If NTSCInt(edTb_cab.Text) = 0 Then
        'Abi non settato, non uso il colore di avviso
        edTb_cab.BackColor = Color.Transparent
      Else
        oMenu.ValCodiceDb(edTb_cab.Text, DittaCorrente, "CAB", "N", , dttTmp, edTb_abi.Text)
        If dttTmp.Rows.Count = 0 OrElse NTSCInt(dttTmp.Rows(0)!abcabichk) = 1 Then
          'Abi non esistente o soppresso, uso il colore di avviso
          edTb_cab.BackColor = Color.Salmon
        Else
          'Abi esistente e non soppresso, non uso il colore di avviso
          edTb_cab.BackColor = Color.Transparent
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class