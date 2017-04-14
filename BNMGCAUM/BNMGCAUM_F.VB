Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGCAUM_F
  Public oCallParams As CLE__CLDP
  Public oCleCaum As CLEMGCAUM
  Public dsCaum As DataSet
  Public dcCaum As BindingSource = New BindingSource

  Public nEditMode As Integer

  Private components As System.ComponentModel.IContainer

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
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
  Public WithEvents pnCaum As NTSInformatica.NTSPanel
  Public WithEvents lbTb_codcaum As NTSInformatica.NTSLabel
  Public WithEvents edTb_codcaum As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_descaum As NTSInformatica.NTSLabel
  Public WithEvents edTb_descaum As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbQuantita1 As NTSInformatica.NTSLabel
  Public WithEvents lbValore1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_carfor As NTSInformatica.NTSLabel
  Public WithEvents lbTb_carvar As NTSInformatica.NTSLabel
  Public WithEvents lbTb_giaini As NTSInformatica.NTSLabel
  Public WithEvents lbTb_causec As NTSInformatica.NTSLabel
  Public WithEvents fmGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents cbTb_esist As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_carfor As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_esist As NTSInformatica.NTSLabel
  Public WithEvents cbTb_carvar As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_giaini As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vcarfor As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vcarvar As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vgiaini As NTSInformatica.NTSComboBox
  Public WithEvents lbTb_rescli As NTSInformatica.NTSLabel
  Public WithEvents lbTb_scacli As NTSInformatica.NTSLabel
  Public WithEvents lbTb_scavar As NTSInformatica.NTSLabel
  Public WithEvents lbTb_resfor As NTSInformatica.NTSLabel
  Public WithEvents lbTb_valoriz As NTSInformatica.NTSLabel
  Public WithEvents cbTb_rescli As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_scacli As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_scavar As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_resfor As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_valoriz As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vrescli As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vscacli As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vscavar As NTSInformatica.NTSComboBox
  Public WithEvents cbTb_vresfor As NTSInformatica.NTSComboBox
  Public WithEvents lbQuantita2 As NTSInformatica.NTSLabel
  Public WithEvents lbValore2 As NTSInformatica.NTSLabel
  Public WithEvents ckTb_dtulsca As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_dtulcar As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_ultpre As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_ultcos As NTSInformatica.NTSCheckBox
  Public WithEvents edTb_causec As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_descaum As NTSInformatica.NTSLabel
  Public WithEvents cbTb_vvaloriz As NTSInformatica.NTSComboBox
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampavideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
#End Region

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim objTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGCAUM", "BEMGCAUM", objTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128049428564715938, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCaum = CType(objTmp, CLEMGCAUM)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BNMGCAUM", strRemoteServer, strRemotePort)
    AddHandler oCleCaum.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCaum.Init(oApp, NTSScript, oMenu.oCleComm, "TABCAUM", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGCAUM_F))
    Me.lbTb_codcaum = New NTSInformatica.NTSLabel
    Me.edTb_codcaum = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_descaum = New NTSInformatica.NTSLabel
    Me.edTb_descaum = New NTSInformatica.NTSTextBoxStr
    Me.lbQuantita1 = New NTSInformatica.NTSLabel
    Me.lbValore1 = New NTSInformatica.NTSLabel
    Me.lbTb_carfor = New NTSInformatica.NTSLabel
    Me.lbTb_carvar = New NTSInformatica.NTSLabel
    Me.lbTb_giaini = New NTSInformatica.NTSLabel
    Me.lbTb_causec = New NTSInformatica.NTSLabel
    Me.fmGroupBox1 = New NTSInformatica.NTSGroupBox
    Me.ckTb_dtulsca = New NTSInformatica.NTSCheckBox
    Me.ckTb_dtulcar = New NTSInformatica.NTSCheckBox
    Me.ckTb_ultpre = New NTSInformatica.NTSCheckBox
    Me.ckTb_ultcos = New NTSInformatica.NTSCheckBox
    Me.cbTb_esist = New NTSInformatica.NTSComboBox
    Me.cbTb_carfor = New NTSInformatica.NTSComboBox
    Me.lbTb_esist = New NTSInformatica.NTSLabel
    Me.cbTb_carvar = New NTSInformatica.NTSComboBox
    Me.cbTb_giaini = New NTSInformatica.NTSComboBox
    Me.cbTb_vcarfor = New NTSInformatica.NTSComboBox
    Me.cbTb_vcarvar = New NTSInformatica.NTSComboBox
    Me.cbTb_vgiaini = New NTSInformatica.NTSComboBox
    Me.lbTb_rescli = New NTSInformatica.NTSLabel
    Me.lbTb_scacli = New NTSInformatica.NTSLabel
    Me.lbTb_scavar = New NTSInformatica.NTSLabel
    Me.lbTb_resfor = New NTSInformatica.NTSLabel
    Me.lbTb_valoriz = New NTSInformatica.NTSLabel
    Me.cbTb_rescli = New NTSInformatica.NTSComboBox
    Me.cbTb_scacli = New NTSInformatica.NTSComboBox
    Me.cbTb_scavar = New NTSInformatica.NTSComboBox
    Me.cbTb_resfor = New NTSInformatica.NTSComboBox
    Me.cbTb_valoriz = New NTSInformatica.NTSComboBox
    Me.cbTb_vrescli = New NTSInformatica.NTSComboBox
    Me.cbTb_vscacli = New NTSInformatica.NTSComboBox
    Me.cbTb_vscavar = New NTSInformatica.NTSComboBox
    Me.cbTb_vresfor = New NTSInformatica.NTSComboBox
    Me.lbQuantita2 = New NTSInformatica.NTSLabel
    Me.lbValore2 = New NTSInformatica.NTSLabel
    Me.edTb_causec = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_descaum = New NTSInformatica.NTSLabel
    Me.cbTb_vvaloriz = New NTSInformatica.NTSComboBox
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
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampavideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnCaum = New NTSInformatica.NTSPanel
    Me.pnControlli = New NTSInformatica.NTSGroupBox
    Me.pn2 = New NTSInformatica.NTSGroupBox
    Me.pn1 = New NTSInformatica.NTSGroupBox
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_codcaum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descaum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmGroupBox1.SuspendLayout()
    CType(Me.ckTb_dtulsca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_dtulcar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_ultpre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_ultcos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_esist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_carfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_carvar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_giaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vcarfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vcarvar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vgiaini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_rescli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_scacli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_scavar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_resfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_valoriz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vrescli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vscacli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vscavar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vresfor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_causec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_vvaloriz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCaum, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCaum.SuspendLayout()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnControlli.SuspendLayout()
    CType(Me.pn2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn2.SuspendLayout()
    CType(Me.pn1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pn1.SuspendLayout()
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
    'lbTb_codcaum
    '
    Me.lbTb_codcaum.AutoSize = True
    Me.lbTb_codcaum.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_codcaum.Location = New System.Drawing.Point(13, 8)
    Me.lbTb_codcaum.Name = "lbTb_codcaum"
    Me.lbTb_codcaum.NTSDbField = ""
    Me.lbTb_codcaum.Size = New System.Drawing.Size(78, 13)
    Me.lbTb_codcaum.TabIndex = 0
    Me.lbTb_codcaum.Text = "Codice causale"
    Me.lbTb_codcaum.Tooltip = ""
    Me.lbTb_codcaum.UseMnemonic = False
    '
    'edTb_codcaum
    '
    Me.edTb_codcaum.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_codcaum.Location = New System.Drawing.Point(151, 5)
    Me.edTb_codcaum.Name = "edTb_codcaum"
    Me.edTb_codcaum.NTSDbField = ""
    Me.edTb_codcaum.NTSFormat = "0"
    Me.edTb_codcaum.NTSForzaVisZoom = False
    Me.edTb_codcaum.NTSOldValue = ""
    Me.edTb_codcaum.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_codcaum.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_codcaum.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_codcaum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_codcaum.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_codcaum.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_codcaum.Properties.AutoHeight = False
    Me.edTb_codcaum.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_codcaum.Properties.MaxLength = 65536
    Me.edTb_codcaum.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_codcaum.Size = New System.Drawing.Size(75, 20)
    Me.edTb_codcaum.TabIndex = 1
    '
    'lbTb_descaum
    '
    Me.lbTb_descaum.AutoSize = True
    Me.lbTb_descaum.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descaum.Location = New System.Drawing.Point(13, 30)
    Me.lbTb_descaum.Name = "lbTb_descaum"
    Me.lbTb_descaum.NTSDbField = ""
    Me.lbTb_descaum.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_descaum.TabIndex = 2
    Me.lbTb_descaum.Text = "Descrizione"
    Me.lbTb_descaum.Tooltip = ""
    Me.lbTb_descaum.UseMnemonic = False
    '
    'edTb_descaum
    '
    Me.edTb_descaum.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descaum.Location = New System.Drawing.Point(151, 27)
    Me.edTb_descaum.Name = "edTb_descaum"
    Me.edTb_descaum.NTSDbField = ""
    Me.edTb_descaum.NTSForzaVisZoom = False
    Me.edTb_descaum.NTSOldValue = ""
    Me.edTb_descaum.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_descaum.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_descaum.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descaum.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descaum.Properties.AutoHeight = False
    Me.edTb_descaum.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descaum.Properties.MaxLength = 65536
    Me.edTb_descaum.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descaum.Size = New System.Drawing.Size(390, 20)
    Me.edTb_descaum.TabIndex = 3
    '
    'lbQuantita1
    '
    Me.lbQuantita1.AutoSize = True
    Me.lbQuantita1.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantita1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbQuantita1.Location = New System.Drawing.Point(167, 4)
    Me.lbQuantita1.Name = "lbQuantita1"
    Me.lbQuantita1.NTSDbField = ""
    Me.lbQuantita1.Size = New System.Drawing.Size(56, 13)
    Me.lbQuantita1.TabIndex = 4
    Me.lbQuantita1.Text = "Quantità"
    Me.lbQuantita1.Tooltip = ""
    Me.lbQuantita1.UseMnemonic = False
    '
    'lbValore1
    '
    Me.lbValore1.AutoSize = True
    Me.lbValore1.BackColor = System.Drawing.Color.Transparent
    Me.lbValore1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbValore1.Location = New System.Drawing.Point(236, 4)
    Me.lbValore1.Name = "lbValore1"
    Me.lbValore1.NTSDbField = ""
    Me.lbValore1.Size = New System.Drawing.Size(43, 13)
    Me.lbValore1.TabIndex = 5
    Me.lbValore1.Text = "Valore"
    Me.lbValore1.Tooltip = ""
    Me.lbValore1.UseMnemonic = False
    '
    'lbTb_carfor
    '
    Me.lbTb_carfor.AutoSize = True
    Me.lbTb_carfor.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_carfor.Location = New System.Drawing.Point(10, 70)
    Me.lbTb_carfor.Name = "lbTb_carfor"
    Me.lbTb_carfor.NTSDbField = ""
    Me.lbTb_carfor.Size = New System.Drawing.Size(99, 13)
    Me.lbTb_carfor.TabIndex = 7
    Me.lbTb_carfor.Text = "Carichi da fornitore"
    Me.lbTb_carfor.Tooltip = ""
    Me.lbTb_carfor.UseMnemonic = False
    '
    'lbTb_carvar
    '
    Me.lbTb_carvar.AutoSize = True
    Me.lbTb_carvar.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_carvar.Location = New System.Drawing.Point(10, 92)
    Me.lbTb_carvar.Name = "lbTb_carvar"
    Me.lbTb_carvar.NTSDbField = ""
    Me.lbTb_carvar.Size = New System.Drawing.Size(59, 13)
    Me.lbTb_carvar.TabIndex = 9
    Me.lbTb_carvar.Text = "Altri carichi"
    Me.lbTb_carvar.Tooltip = ""
    Me.lbTb_carvar.UseMnemonic = False
    '
    'lbTb_giaini
    '
    Me.lbTb_giaini.AutoSize = True
    Me.lbTb_giaini.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_giaini.Location = New System.Drawing.Point(10, 114)
    Me.lbTb_giaini.Name = "lbTb_giaini"
    Me.lbTb_giaini.NTSDbField = ""
    Me.lbTb_giaini.Size = New System.Drawing.Size(84, 13)
    Me.lbTb_giaini.TabIndex = 10
    Me.lbTb_giaini.Text = "Giacenza iniziale"
    Me.lbTb_giaini.Tooltip = ""
    Me.lbTb_giaini.UseMnemonic = False
    '
    'lbTb_causec
    '
    Me.lbTb_causec.AutoSize = True
    Me.lbTb_causec.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_causec.Location = New System.Drawing.Point(13, 52)
    Me.lbTb_causec.Name = "lbTb_causec"
    Me.lbTb_causec.NTSDbField = ""
    Me.lbTb_causec.Size = New System.Drawing.Size(100, 13)
    Me.lbTb_causec.TabIndex = 11
    Me.lbTb_causec.Text = "Causale secondaria"
    Me.lbTb_causec.Tooltip = ""
    Me.lbTb_causec.UseMnemonic = False
    '
    'fmGroupBox1
    '
    Me.fmGroupBox1.AllowDrop = True
    Me.fmGroupBox1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmGroupBox1.Appearance.Options.UseBackColor = True
    Me.fmGroupBox1.Controls.Add(Me.ckTb_dtulsca)
    Me.fmGroupBox1.Controls.Add(Me.ckTb_dtulcar)
    Me.fmGroupBox1.Controls.Add(Me.ckTb_ultpre)
    Me.fmGroupBox1.Controls.Add(Me.ckTb_ultcos)
    Me.fmGroupBox1.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmGroupBox1.Location = New System.Drawing.Point(547, 119)
    Me.fmGroupBox1.Name = "fmGroupBox1"
    Me.fmGroupBox1.ShowCaption = False
    Me.fmGroupBox1.Size = New System.Drawing.Size(148, 87)
    Me.fmGroupBox1.TabIndex = 14
    '
    'ckTb_dtulsca
    '
    Me.ckTb_dtulsca.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckTb_dtulsca.Location = New System.Drawing.Point(5, 65)
    Me.ckTb_dtulsca.Name = "ckTb_dtulsca"
    Me.ckTb_dtulsca.NTSCheckValue = "S"
    Me.ckTb_dtulsca.NTSUnCheckValue = "N"
    Me.ckTb_dtulsca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_dtulsca.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_dtulsca.Properties.AutoHeight = False
    Me.ckTb_dtulsca.Properties.Caption = "Data ultimo scarico"
    Me.ckTb_dtulsca.Size = New System.Drawing.Size(118, 19)
    Me.ckTb_dtulsca.TabIndex = 3
    '
    'ckTb_dtulcar
    '
    Me.ckTb_dtulcar.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_dtulcar.Location = New System.Drawing.Point(5, 45)
    Me.ckTb_dtulcar.Name = "ckTb_dtulcar"
    Me.ckTb_dtulcar.NTSCheckValue = "S"
    Me.ckTb_dtulcar.NTSUnCheckValue = "N"
    Me.ckTb_dtulcar.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_dtulcar.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_dtulcar.Properties.AutoHeight = False
    Me.ckTb_dtulcar.Properties.Caption = "Data ultimo carico"
    Me.ckTb_dtulcar.Size = New System.Drawing.Size(118, 19)
    Me.ckTb_dtulcar.TabIndex = 2
    '
    'ckTb_ultpre
    '
    Me.ckTb_ultpre.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_ultpre.Location = New System.Drawing.Point(5, 25)
    Me.ckTb_ultpre.Name = "ckTb_ultpre"
    Me.ckTb_ultpre.NTSCheckValue = "S"
    Me.ckTb_ultpre.NTSUnCheckValue = "N"
    Me.ckTb_ultpre.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_ultpre.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_ultpre.Properties.AutoHeight = False
    Me.ckTb_ultpre.Properties.Caption = "Ultimo prezzo"
    Me.ckTb_ultpre.Size = New System.Drawing.Size(93, 19)
    Me.ckTb_ultpre.TabIndex = 1
    '
    'ckTb_ultcos
    '
    Me.ckTb_ultcos.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_ultcos.Location = New System.Drawing.Point(5, 5)
    Me.ckTb_ultcos.Name = "ckTb_ultcos"
    Me.ckTb_ultcos.NTSCheckValue = "S"
    Me.ckTb_ultcos.NTSUnCheckValue = "N"
    Me.ckTb_ultcos.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_ultcos.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_ultcos.Properties.AutoHeight = False
    Me.ckTb_ultcos.Properties.Caption = "Ultimo costo"
    Me.ckTb_ultcos.Size = New System.Drawing.Size(93, 19)
    Me.ckTb_ultcos.TabIndex = 0
    '
    'cbTb_esist
    '
    Me.cbTb_esist.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_esist.DataSource = Nothing
    Me.cbTb_esist.DisplayMember = ""
    Me.cbTb_esist.Location = New System.Drawing.Point(3, 25)
    Me.cbTb_esist.Name = "cbTb_esist"
    Me.cbTb_esist.NTSDbField = ""
    Me.cbTb_esist.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_esist.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_esist.Properties.AutoHeight = False
    Me.cbTb_esist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_esist.Properties.DropDownRows = 30
    Me.cbTb_esist.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_esist.SelectedValue = ""
    Me.cbTb_esist.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_esist.TabIndex = 15
    Me.cbTb_esist.ValueMember = ""
    '
    'cbTb_carfor
    '
    Me.cbTb_carfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_carfor.DataSource = Nothing
    Me.cbTb_carfor.DisplayMember = ""
    Me.cbTb_carfor.Location = New System.Drawing.Point(3, 47)
    Me.cbTb_carfor.Name = "cbTb_carfor"
    Me.cbTb_carfor.NTSDbField = ""
    Me.cbTb_carfor.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_carfor.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_carfor.Properties.AutoHeight = False
    Me.cbTb_carfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_carfor.Properties.DropDownRows = 30
    Me.cbTb_carfor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_carfor.SelectedValue = ""
    Me.cbTb_carfor.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_carfor.TabIndex = 16
    Me.cbTb_carfor.ValueMember = ""
    '
    'lbTb_esist
    '
    Me.lbTb_esist.AutoSize = True
    Me.lbTb_esist.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_esist.Location = New System.Drawing.Point(10, 48)
    Me.lbTb_esist.Name = "lbTb_esist"
    Me.lbTb_esist.NTSDbField = ""
    Me.lbTb_esist.Size = New System.Drawing.Size(89, 13)
    Me.lbTb_esist.TabIndex = 17
    Me.lbTb_esist.Text = "Esistenza attuale"
    Me.lbTb_esist.Tooltip = ""
    Me.lbTb_esist.UseMnemonic = False
    '
    'cbTb_carvar
    '
    Me.cbTb_carvar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_carvar.DataSource = Nothing
    Me.cbTb_carvar.DisplayMember = ""
    Me.cbTb_carvar.Location = New System.Drawing.Point(3, 69)
    Me.cbTb_carvar.Name = "cbTb_carvar"
    Me.cbTb_carvar.NTSDbField = ""
    Me.cbTb_carvar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_carvar.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_carvar.Properties.AutoHeight = False
    Me.cbTb_carvar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_carvar.Properties.DropDownRows = 30
    Me.cbTb_carvar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_carvar.SelectedValue = ""
    Me.cbTb_carvar.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_carvar.TabIndex = 19
    Me.cbTb_carvar.ValueMember = ""
    '
    'cbTb_giaini
    '
    Me.cbTb_giaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_giaini.DataSource = Nothing
    Me.cbTb_giaini.DisplayMember = ""
    Me.cbTb_giaini.Location = New System.Drawing.Point(3, 91)
    Me.cbTb_giaini.Name = "cbTb_giaini"
    Me.cbTb_giaini.NTSDbField = ""
    Me.cbTb_giaini.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_giaini.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_giaini.Properties.AutoHeight = False
    Me.cbTb_giaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_giaini.Properties.DropDownRows = 30
    Me.cbTb_giaini.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_giaini.SelectedValue = ""
    Me.cbTb_giaini.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_giaini.TabIndex = 20
    Me.cbTb_giaini.ValueMember = ""
    '
    'cbTb_vcarfor
    '
    Me.cbTb_vcarfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_vcarfor.DataSource = Nothing
    Me.cbTb_vcarfor.DisplayMember = ""
    Me.cbTb_vcarfor.Location = New System.Drawing.Point(59, 47)
    Me.cbTb_vcarfor.Name = "cbTb_vcarfor"
    Me.cbTb_vcarfor.NTSDbField = ""
    Me.cbTb_vcarfor.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vcarfor.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vcarfor.Properties.AutoHeight = False
    Me.cbTb_vcarfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vcarfor.Properties.DropDownRows = 30
    Me.cbTb_vcarfor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vcarfor.SelectedValue = ""
    Me.cbTb_vcarfor.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vcarfor.TabIndex = 21
    Me.cbTb_vcarfor.ValueMember = ""
    '
    'cbTb_vcarvar
    '
    Me.cbTb_vcarvar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_vcarvar.DataSource = Nothing
    Me.cbTb_vcarvar.DisplayMember = ""
    Me.cbTb_vcarvar.Location = New System.Drawing.Point(59, 69)
    Me.cbTb_vcarvar.Name = "cbTb_vcarvar"
    Me.cbTb_vcarvar.NTSDbField = ""
    Me.cbTb_vcarvar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vcarvar.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vcarvar.Properties.AutoHeight = False
    Me.cbTb_vcarvar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vcarvar.Properties.DropDownRows = 30
    Me.cbTb_vcarvar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vcarvar.SelectedValue = ""
    Me.cbTb_vcarvar.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vcarvar.TabIndex = 23
    Me.cbTb_vcarvar.ValueMember = ""
    '
    'cbTb_vgiaini
    '
    Me.cbTb_vgiaini.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_vgiaini.DataSource = Nothing
    Me.cbTb_vgiaini.DisplayMember = ""
    Me.cbTb_vgiaini.Location = New System.Drawing.Point(59, 91)
    Me.cbTb_vgiaini.Name = "cbTb_vgiaini"
    Me.cbTb_vgiaini.NTSDbField = ""
    Me.cbTb_vgiaini.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vgiaini.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vgiaini.Properties.AutoHeight = False
    Me.cbTb_vgiaini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vgiaini.Properties.DropDownRows = 30
    Me.cbTb_vgiaini.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vgiaini.SelectedValue = ""
    Me.cbTb_vgiaini.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vgiaini.TabIndex = 24
    Me.cbTb_vgiaini.ValueMember = ""
    '
    'lbTb_rescli
    '
    Me.lbTb_rescli.AutoSize = True
    Me.lbTb_rescli.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_rescli.Location = New System.Drawing.Point(299, 26)
    Me.lbTb_rescli.Name = "lbTb_rescli"
    Me.lbTb_rescli.NTSDbField = ""
    Me.lbTb_rescli.Size = New System.Drawing.Size(72, 13)
    Me.lbTb_rescli.TabIndex = 25
    Me.lbTb_rescli.Text = "Resi da clienti"
    Me.lbTb_rescli.Tooltip = ""
    Me.lbTb_rescli.UseMnemonic = False
    '
    'lbTb_scacli
    '
    Me.lbTb_scacli.AutoSize = True
    Me.lbTb_scacli.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_scacli.Location = New System.Drawing.Point(299, 48)
    Me.lbTb_scacli.Name = "lbTb_scacli"
    Me.lbTb_scacli.NTSDbField = ""
    Me.lbTb_scacli.Size = New System.Drawing.Size(101, 13)
    Me.lbTb_scacli.TabIndex = 26
    Me.lbTb_scacli.Text = "Scarichi per vendite"
    Me.lbTb_scacli.Tooltip = ""
    Me.lbTb_scacli.UseMnemonic = False
    '
    'lbTb_scavar
    '
    Me.lbTb_scavar.AutoSize = True
    Me.lbTb_scavar.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_scavar.Location = New System.Drawing.Point(299, 69)
    Me.lbTb_scavar.Name = "lbTb_scavar"
    Me.lbTb_scavar.NTSDbField = ""
    Me.lbTb_scavar.Size = New System.Drawing.Size(64, 13)
    Me.lbTb_scavar.TabIndex = 28
    Me.lbTb_scavar.Text = "Altri scarichi"
    Me.lbTb_scavar.Tooltip = ""
    Me.lbTb_scavar.UseMnemonic = False
    '
    'lbTb_resfor
    '
    Me.lbTb_resfor.AutoSize = True
    Me.lbTb_resfor.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_resfor.Location = New System.Drawing.Point(299, 92)
    Me.lbTb_resfor.Name = "lbTb_resfor"
    Me.lbTb_resfor.NTSDbField = ""
    Me.lbTb_resfor.Size = New System.Drawing.Size(77, 13)
    Me.lbTb_resfor.TabIndex = 29
    Me.lbTb_resfor.Text = "Resi a fornitori"
    Me.lbTb_resfor.Tooltip = ""
    Me.lbTb_resfor.UseMnemonic = False
    '
    'lbTb_valoriz
    '
    Me.lbTb_valoriz.AutoSize = True
    Me.lbTb_valoriz.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_valoriz.Location = New System.Drawing.Point(299, 114)
    Me.lbTb_valoriz.Name = "lbTb_valoriz"
    Me.lbTb_valoriz.NTSDbField = ""
    Me.lbTb_valoriz.Size = New System.Drawing.Size(74, 13)
    Me.lbTb_valoriz.TabIndex = 30
    Me.lbTb_valoriz.Text = "Valorizzazione"
    Me.lbTb_valoriz.Tooltip = ""
    Me.lbTb_valoriz.UseMnemonic = False
    '
    'cbTb_rescli
    '
    Me.cbTb_rescli.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_rescli.DataSource = Nothing
    Me.cbTb_rescli.DisplayMember = ""
    Me.cbTb_rescli.Location = New System.Drawing.Point(3, 3)
    Me.cbTb_rescli.Name = "cbTb_rescli"
    Me.cbTb_rescli.NTSDbField = ""
    Me.cbTb_rescli.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_rescli.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_rescli.Properties.AutoHeight = False
    Me.cbTb_rescli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_rescli.Properties.DropDownRows = 30
    Me.cbTb_rescli.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_rescli.SelectedValue = ""
    Me.cbTb_rescli.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_rescli.TabIndex = 31
    Me.cbTb_rescli.ValueMember = ""
    '
    'cbTb_scacli
    '
    Me.cbTb_scacli.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_scacli.DataSource = Nothing
    Me.cbTb_scacli.DisplayMember = ""
    Me.cbTb_scacli.Location = New System.Drawing.Point(3, 25)
    Me.cbTb_scacli.Name = "cbTb_scacli"
    Me.cbTb_scacli.NTSDbField = ""
    Me.cbTb_scacli.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_scacli.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_scacli.Properties.AutoHeight = False
    Me.cbTb_scacli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_scacli.Properties.DropDownRows = 30
    Me.cbTb_scacli.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_scacli.SelectedValue = ""
    Me.cbTb_scacli.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_scacli.TabIndex = 32
    Me.cbTb_scacli.ValueMember = ""
    '
    'cbTb_scavar
    '
    Me.cbTb_scavar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_scavar.DataSource = Nothing
    Me.cbTb_scavar.DisplayMember = ""
    Me.cbTb_scavar.Location = New System.Drawing.Point(3, 47)
    Me.cbTb_scavar.Name = "cbTb_scavar"
    Me.cbTb_scavar.NTSDbField = ""
    Me.cbTb_scavar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_scavar.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_scavar.Properties.AutoHeight = False
    Me.cbTb_scavar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_scavar.Properties.DropDownRows = 30
    Me.cbTb_scavar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_scavar.SelectedValue = ""
    Me.cbTb_scavar.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_scavar.TabIndex = 34
    Me.cbTb_scavar.ValueMember = ""
    '
    'cbTb_resfor
    '
    Me.cbTb_resfor.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_resfor.DataSource = Nothing
    Me.cbTb_resfor.DisplayMember = ""
    Me.cbTb_resfor.Location = New System.Drawing.Point(3, 69)
    Me.cbTb_resfor.Name = "cbTb_resfor"
    Me.cbTb_resfor.NTSDbField = ""
    Me.cbTb_resfor.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_resfor.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_resfor.Properties.AutoHeight = False
    Me.cbTb_resfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_resfor.Properties.DropDownRows = 30
    Me.cbTb_resfor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_resfor.SelectedValue = ""
    Me.cbTb_resfor.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_resfor.TabIndex = 35
    Me.cbTb_resfor.ValueMember = ""
    '
    'cbTb_valoriz
    '
    Me.cbTb_valoriz.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_valoriz.DataSource = Nothing
    Me.cbTb_valoriz.DisplayMember = ""
    Me.cbTb_valoriz.Location = New System.Drawing.Point(3, 91)
    Me.cbTb_valoriz.Name = "cbTb_valoriz"
    Me.cbTb_valoriz.NTSDbField = ""
    Me.cbTb_valoriz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_valoriz.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_valoriz.Properties.AutoHeight = False
    Me.cbTb_valoriz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_valoriz.Properties.DropDownRows = 30
    Me.cbTb_valoriz.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_valoriz.SelectedValue = ""
    Me.cbTb_valoriz.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_valoriz.TabIndex = 36
    Me.cbTb_valoriz.ValueMember = ""
    '
    'cbTb_vrescli
    '
    Me.cbTb_vrescli.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbTb_vrescli.DataSource = Nothing
    Me.cbTb_vrescli.DisplayMember = ""
    Me.cbTb_vrescli.Location = New System.Drawing.Point(60, 3)
    Me.cbTb_vrescli.Name = "cbTb_vrescli"
    Me.cbTb_vrescli.NTSDbField = ""
    Me.cbTb_vrescli.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vrescli.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vrescli.Properties.AutoHeight = False
    Me.cbTb_vrescli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vrescli.Properties.DropDownRows = 30
    Me.cbTb_vrescli.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vrescli.SelectedValue = ""
    Me.cbTb_vrescli.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vrescli.TabIndex = 37
    Me.cbTb_vrescli.ValueMember = ""
    '
    'cbTb_vscacli
    '
    Me.cbTb_vscacli.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbTb_vscacli.DataSource = Nothing
    Me.cbTb_vscacli.DisplayMember = ""
    Me.cbTb_vscacli.Location = New System.Drawing.Point(60, 25)
    Me.cbTb_vscacli.Name = "cbTb_vscacli"
    Me.cbTb_vscacli.NTSDbField = ""
    Me.cbTb_vscacli.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vscacli.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vscacli.Properties.AutoHeight = False
    Me.cbTb_vscacli.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vscacli.Properties.DropDownRows = 30
    Me.cbTb_vscacli.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vscacli.SelectedValue = ""
    Me.cbTb_vscacli.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vscacli.TabIndex = 38
    Me.cbTb_vscacli.ValueMember = ""
    '
    'cbTb_vscavar
    '
    Me.cbTb_vscavar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_vscavar.DataSource = Nothing
    Me.cbTb_vscavar.DisplayMember = ""
    Me.cbTb_vscavar.Location = New System.Drawing.Point(60, 47)
    Me.cbTb_vscavar.Name = "cbTb_vscavar"
    Me.cbTb_vscavar.NTSDbField = ""
    Me.cbTb_vscavar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vscavar.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vscavar.Properties.AutoHeight = False
    Me.cbTb_vscavar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vscavar.Properties.DropDownRows = 30
    Me.cbTb_vscavar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vscavar.SelectedValue = ""
    Me.cbTb_vscavar.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vscavar.TabIndex = 40
    Me.cbTb_vscavar.ValueMember = ""
    '
    'cbTb_vresfor
    '
    Me.cbTb_vresfor.Cursor = System.Windows.Forms.Cursors.Hand
    Me.cbTb_vresfor.DataSource = Nothing
    Me.cbTb_vresfor.DisplayMember = ""
    Me.cbTb_vresfor.Location = New System.Drawing.Point(60, 69)
    Me.cbTb_vresfor.Name = "cbTb_vresfor"
    Me.cbTb_vresfor.NTSDbField = ""
    Me.cbTb_vresfor.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vresfor.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vresfor.Properties.AutoHeight = False
    Me.cbTb_vresfor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vresfor.Properties.DropDownRows = 30
    Me.cbTb_vresfor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vresfor.SelectedValue = ""
    Me.cbTb_vresfor.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vresfor.TabIndex = 41
    Me.cbTb_vresfor.ValueMember = ""
    '
    'lbQuantita2
    '
    Me.lbQuantita2.AutoSize = True
    Me.lbQuantita2.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantita2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbQuantita2.Location = New System.Drawing.Point(403, 4)
    Me.lbQuantita2.Name = "lbQuantita2"
    Me.lbQuantita2.NTSDbField = ""
    Me.lbQuantita2.Size = New System.Drawing.Size(56, 13)
    Me.lbQuantita2.TabIndex = 43
    Me.lbQuantita2.Text = "Quantità"
    Me.lbQuantita2.Tooltip = ""
    Me.lbQuantita2.UseMnemonic = False
    '
    'lbValore2
    '
    Me.lbValore2.AutoSize = True
    Me.lbValore2.BackColor = System.Drawing.Color.Transparent
    Me.lbValore2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbValore2.Location = New System.Drawing.Point(473, 4)
    Me.lbValore2.Name = "lbValore2"
    Me.lbValore2.NTSDbField = ""
    Me.lbValore2.Size = New System.Drawing.Size(43, 13)
    Me.lbValore2.TabIndex = 44
    Me.lbValore2.Text = "Valore"
    Me.lbValore2.Tooltip = ""
    Me.lbValore2.UseMnemonic = False
    '
    'edTb_causec
    '
    Me.edTb_causec.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_causec.Location = New System.Drawing.Point(151, 49)
    Me.edTb_causec.Name = "edTb_causec"
    Me.edTb_causec.NTSDbField = ""
    Me.edTb_causec.NTSFormat = "0"
    Me.edTb_causec.NTSForzaVisZoom = False
    Me.edTb_causec.NTSOldValue = ""
    Me.edTb_causec.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTb_causec.Properties.Appearance.Options.UseBackColor = True
    Me.edTb_causec.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_causec.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_causec.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_causec.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_causec.Properties.AutoHeight = False
    Me.edTb_causec.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_causec.Properties.MaxLength = 65536
    Me.edTb_causec.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_causec.Size = New System.Drawing.Size(75, 20)
    Me.edTb_causec.TabIndex = 45
    '
    'lbXx_descaum
    '
    Me.lbXx_descaum.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_descaum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_descaum.Cursor = System.Windows.Forms.Cursors.Default
    Me.lbXx_descaum.Location = New System.Drawing.Point(232, 49)
    Me.lbXx_descaum.Name = "lbXx_descaum"
    Me.lbXx_descaum.NTSDbField = ""
    Me.lbXx_descaum.Size = New System.Drawing.Size(309, 20)
    Me.lbXx_descaum.TabIndex = 46
    Me.lbXx_descaum.Tooltip = ""
    Me.lbXx_descaum.UseMnemonic = False
    '
    'cbTb_vvaloriz
    '
    Me.cbTb_vvaloriz.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_vvaloriz.DataSource = Nothing
    Me.cbTb_vvaloriz.DisplayMember = ""
    Me.cbTb_vvaloriz.Location = New System.Drawing.Point(60, 91)
    Me.cbTb_vvaloriz.Name = "cbTb_vvaloriz"
    Me.cbTb_vvaloriz.NTSDbField = ""
    Me.cbTb_vvaloriz.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbTb_vvaloriz.Properties.Appearance.Options.UseBackColor = True
    Me.cbTb_vvaloriz.Properties.AutoHeight = False
    Me.cbTb_vvaloriz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_vvaloriz.Properties.DropDownRows = 30
    Me.cbTb_vvaloriz.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_vvaloriz.SelectedValue = ""
    Me.cbTb_vvaloriz.Size = New System.Drawing.Size(50, 20)
    Me.cbTb_vvaloriz.TabIndex = 47
    Me.cbTb_vvaloriz.ValueMember = ""
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbStampa, Me.tlbStampavideo, Me.tlbEsci, Me.tlbGuida, Me.tlbDuplica})
    Me.NtsBarManager1.MaxItemId = 15
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampavideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbSalva.Caption = "Aggiorna"
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
    Me.tlbUltimo.Id = 8
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.GlyphPath = ""
    Me.tlbStampa.Id = 9
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampavideo
    '
    Me.tlbStampavideo.Caption = "StampaVideo"
    Me.tlbStampavideo.Glyph = CType(resources.GetObject("tlbStampavideo.Glyph"), System.Drawing.Image)
    Me.tlbStampavideo.GlyphPath = ""
    Me.tlbStampavideo.Id = 10
    Me.tlbStampavideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampavideo.Name = "tlbStampavideo"
    Me.tlbStampavideo.Visible = True
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
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnCaum
    '
    Me.pnCaum.AllowDrop = True
    Me.pnCaum.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCaum.Appearance.Options.UseBackColor = True
    Me.pnCaum.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCaum.Controls.Add(Me.pnControlli)
    Me.pnCaum.Controls.Add(Me.edTb_causec)
    Me.pnCaum.Controls.Add(Me.lbTb_causec)
    Me.pnCaum.Controls.Add(Me.lbXx_descaum)
    Me.pnCaum.Controls.Add(Me.fmGroupBox1)
    Me.pnCaum.Controls.Add(Me.edTb_descaum)
    Me.pnCaum.Controls.Add(Me.lbTb_descaum)
    Me.pnCaum.Controls.Add(Me.edTb_codcaum)
    Me.pnCaum.Controls.Add(Me.lbTb_codcaum)
    Me.pnCaum.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCaum.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCaum.Location = New System.Drawing.Point(0, 30)
    Me.pnCaum.Name = "pnCaum"
    Me.pnCaum.NTSActiveTrasparency = True
    Me.pnCaum.Size = New System.Drawing.Size(706, 218)
    Me.pnCaum.TabIndex = 50
    Me.pnCaum.Text = "NtsPanel1"
    '
    'pnControlli
    '
    Me.pnControlli.AllowDrop = True
    Me.pnControlli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnControlli.Appearance.Options.UseBackColor = True
    Me.pnControlli.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnControlli.Controls.Add(Me.pn2)
    Me.pnControlli.Controls.Add(Me.lbQuantita2)
    Me.pnControlli.Controls.Add(Me.pn1)
    Me.pnControlli.Controls.Add(Me.lbQuantita1)
    Me.pnControlli.Controls.Add(Me.lbValore1)
    Me.pnControlli.Controls.Add(Me.lbTb_carfor)
    Me.pnControlli.Controls.Add(Me.lbValore2)
    Me.pnControlli.Controls.Add(Me.lbTb_carvar)
    Me.pnControlli.Controls.Add(Me.lbTb_giaini)
    Me.pnControlli.Controls.Add(Me.lbTb_esist)
    Me.pnControlli.Controls.Add(Me.lbTb_rescli)
    Me.pnControlli.Controls.Add(Me.lbTb_valoriz)
    Me.pnControlli.Controls.Add(Me.lbTb_scacli)
    Me.pnControlli.Controls.Add(Me.lbTb_resfor)
    Me.pnControlli.Controls.Add(Me.lbTb_scavar)
    Me.pnControlli.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnControlli.Location = New System.Drawing.Point(12, 75)
    Me.pnControlli.Name = "pnControlli"
    Me.pnControlli.ShowCaption = False
    Me.pnControlli.Size = New System.Drawing.Size(529, 136)
    Me.pnControlli.TabIndex = 60
    '
    'pn2
    '
    Me.pn2.AllowDrop = True
    Me.pn2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn2.Appearance.Options.UseBackColor = True
    Me.pn2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn2.Controls.Add(Me.cbTb_rescli)
    Me.pn2.Controls.Add(Me.cbTb_scacli)
    Me.pn2.Controls.Add(Me.cbTb_scavar)
    Me.pn2.Controls.Add(Me.cbTb_vvaloriz)
    Me.pn2.Controls.Add(Me.cbTb_resfor)
    Me.pn2.Controls.Add(Me.cbTb_valoriz)
    Me.pn2.Controls.Add(Me.cbTb_vrescli)
    Me.pn2.Controls.Add(Me.cbTb_vscacli)
    Me.pn2.Controls.Add(Me.cbTb_vscavar)
    Me.pn2.Controls.Add(Me.cbTb_vresfor)
    Me.pn2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pn2.Location = New System.Drawing.Point(406, 20)
    Me.pn2.Name = "pn2"
    Me.pn2.ShowCaption = False
    Me.pn2.Size = New System.Drawing.Size(120, 114)
    Me.pn2.TabIndex = 62
    '
    'pn1
    '
    Me.pn1.AllowDrop = True
    Me.pn1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pn1.Appearance.Options.UseBackColor = True
    Me.pn1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pn1.Controls.Add(Me.cbTb_esist)
    Me.pn1.Controls.Add(Me.cbTb_vgiaini)
    Me.pn1.Controls.Add(Me.cbTb_vcarvar)
    Me.pn1.Controls.Add(Me.cbTb_vcarfor)
    Me.pn1.Controls.Add(Me.cbTb_giaini)
    Me.pn1.Controls.Add(Me.cbTb_carvar)
    Me.pn1.Controls.Add(Me.cbTb_carfor)
    Me.pn1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pn1.Location = New System.Drawing.Point(170, 20)
    Me.pn1.Name = "pn1"
    Me.pn1.ShowCaption = False
    Me.pn1.Size = New System.Drawing.Size(121, 114)
    Me.pn1.TabIndex = 61
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 14
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'FRMMGCAUM_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(706, 248)
    Me.Controls.Add(Me.pnCaum)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRMMGCAUM_F"
    Me.Text = "CAUSALI DI MAGAZZINO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_codcaum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descaum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmGroupBox1.ResumeLayout(False)
    Me.fmGroupBox1.PerformLayout()
    CType(Me.ckTb_dtulsca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_dtulcar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_ultpre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_ultcos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_esist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_carfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_carvar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_giaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vcarfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vcarvar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vgiaini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_rescli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_scacli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_scavar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_resfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_valoriz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vrescli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vscacli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vscavar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vresfor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_causec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_vvaloriz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCaum, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCaum.ResumeLayout(False)
    Me.pnCaum.PerformLayout()
    CType(Me.pnControlli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnControlli.ResumeLayout(False)
    Me.pnControlli.PerformLayout()
    CType(Me.pn2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn2.ResumeLayout(False)
    CType(Me.pn1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pn1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampavideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'à una immagine prendo quella standard
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      edTb_codcaum.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023161322945, "Codice causale di magazzino"), tabcaum)
      edTb_descaum.NTSSetParam(oMenu, oApp.Tr(Me, 128230023161479118, "Descrizione causale di magazzino"), 30, True)
      edTb_causec.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023161635291, "Codice causale secondaria"), tabcaum)
      ckTb_ultcos.NTSSetParam(oMenu, oApp.Tr(Me, 128230023161791464, "Ultimo costo"), "S", "N")
      ckTb_ultpre.NTSSetParam(oMenu, oApp.Tr(Me, 128230023161947637, "Ultimo prezzo"), "S", "N")
      ckTb_dtulcar.NTSSetParam(oMenu, oApp.Tr(Me, 128230023162103810, "Data ultimo carico"), "S", "N")
      ckTb_dtulsca.NTSSetParam(oMenu, oApp.Tr(Me, 128230023162259983, "Data ultimo scarico"), "S", "N")
      cbTb_esist.NTSSetParam(oApp.Tr(Me, 128230023162572329, "Esistenza attuale"))
      cbTb_carfor.NTSSetParam(oApp.Tr(Me, 128230023162728502, "Carichi da fornitore"))
      cbTb_carvar.NTSSetParam(oApp.Tr(Me, 128230023163040848, "Altri carichi"))
      cbTb_giaini.NTSSetParam(oApp.Tr(Me, 128230023163197021, "Giacenza iniziale"))
      cbTb_rescli.NTSSetParam(oApp.Tr(Me, 128230023163353194, "Resi da clienti"))
      cbTb_scacli.NTSSetParam(oApp.Tr(Me, 128230023163509367, "Scarichi per vendite"))
      cbTb_scavar.NTSSetParam(oApp.Tr(Me, 128230023163821713, "Altri scarichi"))
      cbTb_resfor.NTSSetParam(oApp.Tr(Me, 128230023163977886, "Resi a fornitori"))
      cbTb_valoriz.NTSSetParam(oApp.Tr(Me, 128230023164134059, "Valorizzazione"))
      '--------------------------------------------------------------------------------------------------------------
      edTb_descaum.NTSForzaVisZoom = True
      edTb_codcaum.NTSSetParamZoom("")
      edTb_causec.NTSSetParamZoom("ZOOMTABCAUM")
      '--------------------------------------------------------------------------------------------------------------
      edTb_codcaum.NTSSetRichiesto()
      edTb_descaum.NTSSetRichiesto()
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi Form"
  Public Overridable Sub FRMMGCAUM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaCombo()
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If oCleCaum.Apri(DittaCorrente, dsCaum) Then
        dcCaum.DataSource = dsCaum.Tables("TABCAUM")
        dsCaum.AcceptChanges()
        If dsCaum.Tables("TABCAUM").Rows.Count > 0 Then
          SetStato(1)
        Else
          tlbNuovo_ItemClick(Nothing, Nothing)
        End If
        '------------------------------------------------------------------------------------------------------------
        Bindcontrols()
        '------------------------------------------------------------------------------------------------------------
        GctlSetRoules()
        '------------------------------------------------------------------------------------------------------------
        If Not oCallParams Is Nothing Then
          If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
            If dsCaum.Tables("TABCAUM").Rows.Count = 1 And dsCaum.Tables("TABCAUM").Rows(0).RowState = DataRowState.Added Then
            Else
              tlbNuovo_ItemClick(Me, Nothing)
            End If
          ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
            For i As Integer = 0 To dcCaum.List.Count - 1
              If CType(dcCaum.Item(i), DataRowView)!tb_codcaum.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
                dcCaum.Position = i
                Exit For
              End If
            Next
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
      Else
        Me.Close()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGCAUM_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGCAUM_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    Try
      dcCaum.Dispose()
      If Not dsCaum Is Nothing Then dsCaum.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      If Not Salva() Then Return
      dcCaum.MoveFirst()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      If Not Salva() Then Return
      dcCaum.MovePrevious()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      If Not Salva() Then Return
      dcCaum.MoveNext()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      If Not Salva() Then Return
      dcCaum.MoveLast()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() Then
        oCleCaum.Nuovo()
        dcCaum.MoveLast()
        SetStato(2)
        '------------------------------------------------------------------------------------------------------------
        Me.GctlApplicaDefaultValue()
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim frmDuar As FRM__DUAR = Nothing
    Dim strCodOld As String = ""
    Try
      If Not Salva() Then Return

      'test su riga valida
      If edTb_codcaum.Text = "0" Or edTb_codcaum.Text = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130723482390952496, "Posizionarsi su un record valido"))
        Return
      Else
        strCodOld = edTb_codcaum.Text
      End If

      frmDuar = CType(NTSNewFormModal("FRM__DUAR"), FRM__DUAR)
      frmDuar.Init(oMenu, Nothing, DittaCorrente)
      frmDuar.oCleCaum = oCleCaum
      frmDuar.ShowDialog()
      If frmDuar.bOk = False Or frmDuar.strCodDupl = "" Then
        'annullato
        Return
      End If

      If Not oCleCaum.Duplica(frmDuar.strCodDupl, strCodOld) Then
        Return
      End If

      dcCaum.MoveLast()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmDuar Is Nothing Then frmDuar.Dispose()
      frmDuar = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Windows.Forms.DialogResult.Yes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128165218220725093, "Cancellare la causale di magazzino?")) Then
        If dsCaum.Tables("TABCAUM").Rows.Count = 1 Then NTSFormClearDataBinding(Me)
        dcCaum.RemoveAt(dcCaum.Position)
        If oCleCaum.Salva(True) Then
          If dsCaum.Tables("TABCAUM").Rows.Count > 0 Then
            SetStato(1)
          Else
            tlbNuovo_ItemClick(Nothing, Nothing)
            Bindcontrols()
            SetStato(2)
          End If
        Else
          dcCaum.ResetItem(dcCaum.Position)
          Bindcontrols()
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim dRes As DialogResult

    Try
      '--------------------------------------------------------------------------------------------------------------
      If sender Is Nothing Then
        dRes = Windows.Forms.DialogResult.Yes
      Else
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128165218583109093, "Ripristinare le modifiche apportate alla causale di magazzino?"))
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Windows.Forms.DialogResult.Yes = dRes Then
        If dsCaum.Tables("TABCAUM").Rows.Count = 1 Then NTSFormClearDataBinding(Me)
        oCleCaum.Ripristina(dcCaum.Position, dcCaum.Filter)
        If dsCaum.Tables("TABCAUM").Rows.Count > 0 Then
          SetStato(1)
        Else
          tlbNuovo_ItemClick(Nothing, Nothing)
          Bindcontrols()
          SetStato(2)
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edTb_descaum.Focused Then
        Dim oParam As New CLE__PATB
        Me.ValidaLastControl()
        If oCleCaum.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128167130727399428, "Salvare o ripristinare le modifiche prima di selezionare una nuova causale di magazzino"))
          Return
        End If
        NTSZOOM.strIn = edTb_codcaum.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCAUM", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edTb_codcaum.Text Then
          For i As Integer = 0 To dcCaum.List.Count - 1
            If CType(dcCaum.Item(i), DataRowView)!tb_codcaum.ToString = NTSZOOM.strIn Then
              dcCaum.Position = i
              Return
            End If
          Next
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      NTSCallStandardZoom()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampavideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    Try
      SendKeys.SendWait("{F1}")
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#End Region

#Region "TextBox"
  Public Overridable Sub edTb_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_descaum.Leave, edTb_causec.Leave
    tlbZoom.Enabled = False
  End Sub
  Public Overridable Sub edTb_descaum_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_descaum.Enter
    If nEditMode = 1 Then tlbZoom.Enabled = True
  End Sub
  Public Overridable Sub edTb_causec_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_causec.Enter
    tlbZoom.Enabled = True
  End Sub
#End Region

  Public Overridable Sub Bindcontrols()
    Try
      '--------------------------------------------------------------------------------------------------------------
      NTSFormClearDataBinding(Me)
      '--------------------------------------------------------------------------------------------------------------
      edTb_codcaum.NTSDbField = "tabcaum.tb_codcaum"
      edTb_descaum.NTSDbField = "tabcaum.tb_descaum"
      edTb_causec.NTSDbField = "tabcaum.tb_causec"
      lbXx_descaum.NTSDbField = "tabcaum.xx_descaum"
      cbTb_esist.NTSDbField = "tabcaum.tb_esist"
      cbTb_carfor.NTSDbField = "tabcaum.tb_carfor"
      cbTb_vcarfor.NTSDbField = "tabcaum.tb_vcarfor"
      cbTb_carvar.NTSDbField = "tabcaum.tb_carvar"
      cbTb_vcarvar.NTSDbField = "tabcaum.tb_vcarvar"
      cbTb_rescli.NTSDbField = "tabcaum.tb_rescli"
      cbTb_vrescli.NTSDbField = "tabcaum.tb_vrescli"
      cbTb_scacli.NTSDbField = "tabcaum.tb_scacli"
      cbTb_vscacli.NTSDbField = "tabcaum.tb_vscacli"
      cbTb_scavar.NTSDbField = "tabcaum.tb_scavar"
      cbTb_vscavar.NTSDbField = "tabcaum.tb_vscavar"
      cbTb_resfor.NTSDbField = "tabcaum.tb_resfor"
      cbTb_vresfor.NTSDbField = "tabcaum.tb_vresfor"
      cbTb_giaini.NTSDbField = "tabcaum.tb_giaini"
      cbTb_vgiaini.NTSDbField = "tabcaum.tb_vgiaini"
      cbTb_valoriz.NTSDbField = "tabcaum.tb_valoriz"
      cbTb_vvaloriz.NTSDbField = "tabcaum.tb_vvaloriz"
      ckTb_ultcos.NTSText.NTSDbField = "tabcaum.tb_ultcos"
      ckTb_ultpre.NTSText.NTSDbField = "tabcaum.tb_ultpre"
      ckTb_dtulcar.NTSText.NTSDbField = "tabcaum.tb_dtulcar"
      ckTb_dtulsca.NTSText.NTSDbField = "tabcaum.tb_dtulsca"
      '--------------------------------------------------------------------------------------------------------------
      NTSFormAddDataBinding(dcCaum, Me)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Try
      Dim dttEsist As New DataTable()
      Dim dttCarfor As New DataTable()
      Dim dttVcarfor As New DataTable()
      Dim dttCarpro As New DataTable()
      Dim dttVcarpro As New DataTable()
      Dim dttCarvar As New DataTable()
      Dim dttVcarvar As New DataTable()
      Dim dttGiaini As New DataTable()
      Dim dttVgiaini As New DataTable()
      Dim dttRescli As New DataTable()
      Dim dttVrescli As New DataTable()
      Dim dttScacli As New DataTable()
      Dim dttVscacli As New DataTable()
      Dim dttScapro As New DataTable()
      Dim dttVscapro As New DataTable()
      Dim dttScavar As New DataTable()
      Dim dttVscavar As New DataTable()
      Dim dttResfor As New DataTable()
      Dim dttVresfor As New DataTable()
      Dim dttValoriz As New DataTable()
      Dim dttVvaloriz As New DataTable()

      'Esistenza
      dttEsist.Columns.Add("cod", GetType(Integer))
      dttEsist.Columns.Add("val", GetType(String))
      dttEsist.Rows.Add(New Object() {-1, "-"})
      dttEsist.Rows.Add(New Object() {0, "(no)"})
      dttEsist.Rows.Add(New Object() {1, "+"})
      dttEsist.AcceptChanges()
      cbTb_esist.DataSource = dttEsist
      cbTb_esist.ValueMember = "cod"
      cbTb_esist.DisplayMember = "val"

      'Carichi da fornitore
      dttCarfor.Columns.Add("cod", GetType(Integer))
      dttCarfor.Columns.Add("val", GetType(String))
      dttCarfor.Rows.Add(New Object() {-1, "-"})
      dttCarfor.Rows.Add(New Object() {0, "(no)"})
      dttCarfor.Rows.Add(New Object() {1, "+"})
      dttCarfor.AcceptChanges()
      cbTb_carfor.DataSource = dttCarfor
      cbTb_carfor.ValueMember = "cod"
      cbTb_carfor.DisplayMember = "val"
      '---
      dttVcarfor.Columns.Add("cod", GetType(Integer))
      dttVcarfor.Columns.Add("val", GetType(String))
      dttVcarfor.Rows.Add(New Object() {-1, "-"})
      dttVcarfor.Rows.Add(New Object() {0, "(no)"})
      dttVcarfor.Rows.Add(New Object() {1, "+"})
      dttVcarfor.AcceptChanges()
      cbTb_vcarfor.DataSource = dttVcarfor
      cbTb_vcarfor.ValueMember = "cod"
      cbTb_vcarfor.DisplayMember = "val"

      'Carichi da produzione
      dttCarpro.Columns.Add("cod", GetType(Integer))
      dttCarpro.Columns.Add("val", GetType(String))
      dttCarpro.Rows.Add(New Object() {-1, "-"})
      dttCarpro.Rows.Add(New Object() {0, "(no)"})
      dttCarpro.Rows.Add(New Object() {1, "+"})
      dttCarpro.AcceptChanges()
      '---
      dttVcarpro.Columns.Add("cod", GetType(Integer))
      dttVcarpro.Columns.Add("val", GetType(String))
      dttVcarpro.Rows.Add(New Object() {-1, "-"})
      dttVcarpro.Rows.Add(New Object() {0, "(no)"})
      dttVcarpro.Rows.Add(New Object() {1, "+"})
      dttVcarpro.AcceptChanges()

      'Altri carichi
      dttCarvar.Columns.Add("cod", GetType(Integer))
      dttCarvar.Columns.Add("val", GetType(String))
      dttCarvar.Rows.Add(New Object() {-1, "-"})
      dttCarvar.Rows.Add(New Object() {0, "(no)"})
      dttCarvar.Rows.Add(New Object() {1, "+"})
      dttCarvar.AcceptChanges()
      cbTb_carvar.DataSource = dttCarvar
      cbTb_carvar.ValueMember = "cod"
      cbTb_carvar.DisplayMember = "val"
      '---
      dttVcarvar.Columns.Add("cod", GetType(Integer))
      dttVcarvar.Columns.Add("val", GetType(String))
      dttVcarvar.Rows.Add(New Object() {-1, "-"})
      dttVcarvar.Rows.Add(New Object() {0, "(no)"})
      dttVcarvar.Rows.Add(New Object() {1, "+"})
      dttVcarvar.AcceptChanges()
      cbTb_vcarvar.DataSource = dttVcarvar
      cbTb_vcarvar.ValueMember = "cod"
      cbTb_vcarvar.DisplayMember = "val"

      'Giacenza iniziale
      dttGiaini.Columns.Add("cod", GetType(Integer))
      dttGiaini.Columns.Add("val", GetType(String))
      dttGiaini.Rows.Add(New Object() {-1, "-"})
      dttGiaini.Rows.Add(New Object() {0, "(no)"})
      dttGiaini.Rows.Add(New Object() {1, "+"})
      dttGiaini.AcceptChanges()
      cbTb_giaini.DataSource = dttGiaini
      cbTb_giaini.ValueMember = "cod"
      cbTb_giaini.DisplayMember = "val"
      '---
      dttVgiaini.Columns.Add("cod", GetType(Integer))
      dttVgiaini.Columns.Add("val", GetType(String))
      dttVgiaini.Rows.Add(New Object() {-1, "-"})
      dttVgiaini.Rows.Add(New Object() {0, "(no)"})
      dttVgiaini.Rows.Add(New Object() {1, "+"})
      dttVgiaini.AcceptChanges()
      cbTb_vgiaini.DataSource = dttVgiaini
      cbTb_vgiaini.ValueMember = "cod"
      cbTb_vgiaini.DisplayMember = "val"

      'Resi da clienti
      dttRescli.Columns.Add("cod", GetType(Integer))
      dttRescli.Columns.Add("val", GetType(String))
      dttRescli.Rows.Add(New Object() {-1, "-"})
      dttRescli.Rows.Add(New Object() {0, "(no)"})
      dttRescli.Rows.Add(New Object() {1, "+"})
      dttRescli.AcceptChanges()
      cbTb_rescli.DataSource = dttRescli
      cbTb_rescli.ValueMember = "cod"
      cbTb_rescli.DisplayMember = "val"
      '---
      dttVrescli.Columns.Add("cod", GetType(Integer))
      dttVrescli.Columns.Add("val", GetType(String))
      dttVrescli.Rows.Add(New Object() {-1, "-"})
      dttVrescli.Rows.Add(New Object() {0, "(no)"})
      dttVrescli.Rows.Add(New Object() {1, "+"})
      dttVrescli.AcceptChanges()
      cbTb_vrescli.DataSource = dttVrescli
      cbTb_vrescli.ValueMember = "cod"
      cbTb_vrescli.DisplayMember = "val"

      'Scarichi per vendite
      dttScacli.Columns.Add("cod", GetType(Integer))
      dttScacli.Columns.Add("val", GetType(String))
      dttScacli.Rows.Add(New Object() {-1, "-"})
      dttScacli.Rows.Add(New Object() {0, "(no)"})
      dttScacli.Rows.Add(New Object() {1, "+"})
      dttScacli.AcceptChanges()
      cbTb_scacli.DataSource = dttScacli
      cbTb_scacli.ValueMember = "cod"
      cbTb_scacli.DisplayMember = "val"
      '---
      dttVscacli.Columns.Add("cod", GetType(Integer))
      dttVscacli.Columns.Add("val", GetType(String))
      dttVscacli.Rows.Add(New Object() {-1, "-"})
      dttVscacli.Rows.Add(New Object() {0, "(no)"})
      dttVscacli.Rows.Add(New Object() {1, "+"})
      dttVscacli.AcceptChanges()
      cbTb_vscacli.DataSource = dttVscacli
      cbTb_vscacli.ValueMember = "cod"
      cbTb_vscacli.DisplayMember = "val"

      'Scarichi a produzione
      dttScapro.Columns.Add("cod", GetType(Integer))
      dttScapro.Columns.Add("val", GetType(String))
      dttScapro.Rows.Add(New Object() {-1, "-"})
      dttScapro.Rows.Add(New Object() {0, "(no)"})
      dttScapro.Rows.Add(New Object() {1, "+"})
      dttScapro.AcceptChanges()
      '---
      dttVscapro.Columns.Add("cod", GetType(Integer))
      dttVscapro.Columns.Add("val", GetType(String))
      dttVscapro.Rows.Add(New Object() {-1, "-"})
      dttVscapro.Rows.Add(New Object() {0, "(no)"})
      dttVscapro.Rows.Add(New Object() {1, "+"})
      dttVscapro.AcceptChanges()

      'Altri scarichi
      dttScavar.Columns.Add("cod", GetType(Integer))
      dttScavar.Columns.Add("val", GetType(String))
      dttScavar.Rows.Add(New Object() {-1, "-"})
      dttScavar.Rows.Add(New Object() {0, "(no)"})
      dttScavar.Rows.Add(New Object() {1, "+"})
      dttScavar.AcceptChanges()
      cbTb_scavar.DataSource = dttScavar
      cbTb_scavar.ValueMember = "cod"
      cbTb_scavar.DisplayMember = "val"
      '---
      dttVscavar.Columns.Add("cod", GetType(Integer))
      dttVscavar.Columns.Add("val", GetType(String))
      dttVscavar.Rows.Add(New Object() {-1, "-"})
      dttVscavar.Rows.Add(New Object() {0, "(no)"})
      dttVscavar.Rows.Add(New Object() {1, "+"})
      dttVscavar.AcceptChanges()
      cbTb_vscavar.DataSource = dttVscavar
      cbTb_vscavar.ValueMember = "cod"
      cbTb_vscavar.DisplayMember = "val"

      'Resi a fornitori
      dttResfor.Columns.Add("cod", GetType(Integer))
      dttResfor.Columns.Add("val", GetType(String))
      dttResfor.Rows.Add(New Object() {-1, "-"})
      dttResfor.Rows.Add(New Object() {0, "(no)"})
      dttResfor.Rows.Add(New Object() {1, "+"})
      dttResfor.AcceptChanges()
      cbTb_resfor.DataSource = dttResfor
      cbTb_resfor.ValueMember = "cod"
      cbTb_resfor.DisplayMember = "val"
      '---
      dttVresfor.Columns.Add("cod", GetType(Integer))
      dttVresfor.Columns.Add("val", GetType(String))
      dttVresfor.Rows.Add(New Object() {-1, "-"})
      dttVresfor.Rows.Add(New Object() {0, "(no)"})
      dttVresfor.Rows.Add(New Object() {1, "+"})
      dttVresfor.AcceptChanges()
      cbTb_vresfor.DataSource = dttVresfor
      cbTb_vresfor.ValueMember = "cod"
      cbTb_vresfor.DisplayMember = "val"

      'Valorizzazione
      dttValoriz.Columns.Add("cod", GetType(Integer))
      dttValoriz.Columns.Add("val", GetType(String))
      dttValoriz.Rows.Add(New Object() {-1, "-"})
      dttValoriz.Rows.Add(New Object() {0, "(no)"})
      dttValoriz.Rows.Add(New Object() {1, "+"})
      dttValoriz.AcceptChanges()
      cbTb_valoriz.DataSource = dttValoriz
      cbTb_valoriz.ValueMember = "cod"
      cbTb_valoriz.DisplayMember = "val"
      '---
      dttVvaloriz.Columns.Add("cod", GetType(Integer))
      dttVvaloriz.Columns.Add("val", GetType(String))
      dttVvaloriz.Rows.Add(New Object() {-1, "-"})
      dttVvaloriz.Rows.Add(New Object() {0, "(no)"})
      dttVvaloriz.Rows.Add(New Object() {1, "+"})
      dttVvaloriz.AcceptChanges()
      cbTb_vvaloriz.DataSource = dttVvaloriz
      cbTb_vvaloriz.ValueMember = "cod"
      cbTb_vvaloriz.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If oCleCaum.RecordIsChanged Then
        If GctlControllaOutNotEqual() = False Then Return False
        If Not oCleCaum.ControlliPreSalva(ckTb_ultcos.Checked, ckTb_dtulcar.Checked, " ", " ", NTSCStr(cbTb_esist.EditValue)) Then Return False
        Select Case oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128165232379161693, "Salvo la causale di magazzino?"))
          Case System.Windows.Forms.DialogResult.Cancel : Return False
          Case System.Windows.Forms.DialogResult.Yes
            If Not oCleCaum.Salva(False) Then Return False
            SetStato(1)
          Case System.Windows.Forms.DialogResult.No : tlbRipristina_ItemClick(Nothing, Nothing)
        End Select
      End If
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      '--------------------------------------------------------------------------------------------------------------
      nEditMode = nSetStato
      '--------------------------------------------------------------------------------------------------------------
      If nSetStato = 1 Then
        edTb_codcaum.Enabled = False
        edTb_descaum.NTSForzaVisZoom = True
        edTb_descaum.Focus()
      Else
        edTb_codcaum.Enabled = True
        edTb_codcaum.Focus()
        edTb_descaum.NTSForzaVisZoom = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva() Then Return
      '--------------------------------------------------------------------------------------------------------------
      strCrpe = ""
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BNMGCAUM", "Reports1", " ", 0, nDestin, "BSMGCAUM.RPT", False, "CAUSALI DI MAGAZZINO", False)
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class