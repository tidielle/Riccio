Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGRCPR

#Region "VARIABILI"
  Public oCleRcpr As CLEMGRCPR
  Public oCallParams As CLE__CLDP

  Public dPrezzoNew(9) As Decimal
  Public dPrezzoOld(9) As Decimal
  Public dtDataOld(9) As Date
  Public dPrezzoNewTxt(9) As Decimal 'per leggere i reali valori digitati nelle textbox

  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDescodart As NTSInformatica.NTSLabel
  Public WithEvents lbPrezzoBase As NTSInformatica.NTSLabel
  Public WithEvents edPrezzoBase As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbUltimoCosto As NTSInformatica.NTSLabel
  Public WithEvents edUltimoCosto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbScostamento As NTSInformatica.NTSLabel
  Public WithEvents edScostamento As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbLabel1 As NTSInformatica.NTSLabel
  Public WithEvents lbDataEffetto As NTSInformatica.NTSLabel
  Public WithEvents edDataEffetto As NTSInformatica.NTSTextBoxData
  Public WithEvents lbQuantIni As NTSInformatica.NTSLabel
  Public WithEvents edQuantIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbQuantFin As NTSInformatica.NTSLabel
  Public WithEvents edQuantFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRicMar As NTSInformatica.NTSLabel
  Public WithEvents lbArrotondamento As NTSInformatica.NTSLabel
  Public WithEvents edArrotondamento As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPercRicMar As NTSInformatica.NTSLabel
  Public WithEvents edPerc1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPerc2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRelazioneListini As NTSInformatica.NTSLabel
  Public WithEvents edRelazioneListini As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmListini As NTSInformatica.NTSGroupBox
  Public WithEvents lbLabel5 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel4 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel3 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel2 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel13 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel12 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel11 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel10 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel9 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel8 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel7 As NTSInformatica.NTSLabel
  Public WithEvents lbLabel6 As NTSInformatica.NTSLabel
  Public WithEvents edPrezzoNew6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoNew7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPrezzoOld6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDataPrec1 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec8 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec7 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec6 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec5 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec4 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec3 As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataPrec2 As NTSInformatica.NTSTextBoxData
  Public WithEvents cmdRicalcola As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnPanel1 As NTSInformatica.NTSPanel
  Public WithEvents edRicMar As NTSInformatica.NTSTextBoxStr

  Private components As System.ComponentModel.IContainer
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGRCPR", "BEMGRCPR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128578134131901506, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleRcpr = CType(oTmp, CLEMGRCPR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGRCPR", strRemoteServer, strRemotePort)
    AddHandler oCleRcpr.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleRcpr.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbDescodart = New NTSInformatica.NTSLabel
    Me.lbPrezzoBase = New NTSInformatica.NTSLabel
    Me.edPrezzoBase = New NTSInformatica.NTSTextBoxNum
    Me.lbUltimoCosto = New NTSInformatica.NTSLabel
    Me.edUltimoCosto = New NTSInformatica.NTSTextBoxNum
    Me.lbScostamento = New NTSInformatica.NTSLabel
    Me.edScostamento = New NTSInformatica.NTSTextBoxNum
    Me.lbLabel1 = New NTSInformatica.NTSLabel
    Me.lbDataEffetto = New NTSInformatica.NTSLabel
    Me.edDataEffetto = New NTSInformatica.NTSTextBoxData
    Me.lbQuantIni = New NTSInformatica.NTSLabel
    Me.edQuantIni = New NTSInformatica.NTSTextBoxNum
    Me.lbQuantFin = New NTSInformatica.NTSLabel
    Me.edQuantFin = New NTSInformatica.NTSTextBoxNum
    Me.lbRicMar = New NTSInformatica.NTSLabel
    Me.lbArrotondamento = New NTSInformatica.NTSLabel
    Me.edArrotondamento = New NTSInformatica.NTSTextBoxNum
    Me.lbPercRicMar = New NTSInformatica.NTSLabel
    Me.edPerc1 = New NTSInformatica.NTSTextBoxNum
    Me.edPerc2 = New NTSInformatica.NTSTextBoxNum
    Me.lbRelazioneListini = New NTSInformatica.NTSLabel
    Me.edRelazioneListini = New NTSInformatica.NTSTextBoxStr
    Me.fmListini = New NTSInformatica.NTSGroupBox
    Me.edDataPrec8 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec7 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec6 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec5 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec4 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec3 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec2 = New NTSInformatica.NTSTextBoxData
    Me.edDataPrec1 = New NTSInformatica.NTSTextBoxData
    Me.edPrezzoOld8 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld7 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld6 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld5 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld4 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld3 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld2 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoOld1 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew8 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew7 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew6 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew5 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew4 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew3 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew2 = New NTSInformatica.NTSTextBoxNum
    Me.edPrezzoNew1 = New NTSInformatica.NTSTextBoxNum
    Me.lbLabel13 = New NTSInformatica.NTSLabel
    Me.lbLabel12 = New NTSInformatica.NTSLabel
    Me.lbLabel11 = New NTSInformatica.NTSLabel
    Me.lbLabel10 = New NTSInformatica.NTSLabel
    Me.lbLabel9 = New NTSInformatica.NTSLabel
    Me.lbLabel8 = New NTSInformatica.NTSLabel
    Me.lbLabel7 = New NTSInformatica.NTSLabel
    Me.lbLabel6 = New NTSInformatica.NTSLabel
    Me.lbLabel5 = New NTSInformatica.NTSLabel
    Me.lbLabel4 = New NTSInformatica.NTSLabel
    Me.lbLabel3 = New NTSInformatica.NTSLabel
    Me.lbLabel2 = New NTSInformatica.NTSLabel
    Me.cmdRicalcola = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnPanel1 = New NTSInformatica.NTSPanel
    Me.edRicMar = New NTSInformatica.NTSTextBoxStr
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoBase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUltimoCosto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edScostamento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataEffetto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuantIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuantFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArrotondamento.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPerc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPerc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRelazioneListini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmListini, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmListini.SuspendLayout()
    CType(Me.edDataPrec8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataPrec1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoOld1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPrezzoNew1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPanel1.SuspendLayout()
    CType(Me.edRicMar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(13, 15)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(78, 13)
    Me.lbCodart.TabIndex = 0
    Me.lbCodart.Text = "Codice Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.EditValue = ""
    Me.edCodart.Enabled = False
    Me.edCodart.Location = New System.Drawing.Point(121, 12)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(150, 20)
    Me.edCodart.TabIndex = 1
    '
    'lbDescodart
    '
    Me.lbDescodart.BackColor = System.Drawing.Color.Transparent
    Me.lbDescodart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescodart.Location = New System.Drawing.Point(277, 12)
    Me.lbDescodart.Name = "lbDescodart"
    Me.lbDescodart.NTSDbField = ""
    Me.lbDescodart.Size = New System.Drawing.Size(359, 20)
    Me.lbDescodart.TabIndex = 2
    Me.lbDescodart.Tooltip = ""
    Me.lbDescodart.UseMnemonic = False
    '
    'lbPrezzoBase
    '
    Me.lbPrezzoBase.AutoSize = True
    Me.lbPrezzoBase.BackColor = System.Drawing.Color.Transparent
    Me.lbPrezzoBase.Location = New System.Drawing.Point(13, 38)
    Me.lbPrezzoBase.Name = "lbPrezzoBase"
    Me.lbPrezzoBase.NTSDbField = ""
    Me.lbPrezzoBase.Size = New System.Drawing.Size(65, 13)
    Me.lbPrezzoBase.TabIndex = 3
    Me.lbPrezzoBase.Text = "Prezzo Base"
    Me.lbPrezzoBase.Tooltip = ""
    Me.lbPrezzoBase.UseMnemonic = False
    '
    'edPrezzoBase
    '
    Me.edPrezzoBase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoBase.Location = New System.Drawing.Point(121, 35)
    Me.edPrezzoBase.Name = "edPrezzoBase"
    Me.edPrezzoBase.NTSDbField = ""
    Me.edPrezzoBase.NTSFormat = "0"
    Me.edPrezzoBase.NTSForzaVisZoom = False
    Me.edPrezzoBase.NTSOldValue = ""
    Me.edPrezzoBase.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoBase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoBase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoBase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoBase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoBase.Properties.MaxLength = 65536
    Me.edPrezzoBase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoBase.Size = New System.Drawing.Size(100, 20)
    Me.edPrezzoBase.TabIndex = 4
    '
    'lbUltimoCosto
    '
    Me.lbUltimoCosto.AutoSize = True
    Me.lbUltimoCosto.BackColor = System.Drawing.Color.Transparent
    Me.lbUltimoCosto.Location = New System.Drawing.Point(284, 38)
    Me.lbUltimoCosto.Name = "lbUltimoCosto"
    Me.lbUltimoCosto.NTSDbField = ""
    Me.lbUltimoCosto.Size = New System.Drawing.Size(67, 13)
    Me.lbUltimoCosto.TabIndex = 5
    Me.lbUltimoCosto.Text = "Ultimo Costo"
    Me.lbUltimoCosto.Tooltip = ""
    Me.lbUltimoCosto.UseMnemonic = False
    '
    'edUltimoCosto
    '
    Me.edUltimoCosto.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edUltimoCosto.Enabled = False
    Me.edUltimoCosto.Location = New System.Drawing.Point(379, 35)
    Me.edUltimoCosto.Name = "edUltimoCosto"
    Me.edUltimoCosto.NTSDbField = ""
    Me.edUltimoCosto.NTSFormat = "0"
    Me.edUltimoCosto.NTSForzaVisZoom = False
    Me.edUltimoCosto.NTSOldValue = ""
    Me.edUltimoCosto.Properties.Appearance.Options.UseTextOptions = True
    Me.edUltimoCosto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edUltimoCosto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUltimoCosto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUltimoCosto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUltimoCosto.Properties.MaxLength = 65536
    Me.edUltimoCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUltimoCosto.Size = New System.Drawing.Size(86, 20)
    Me.edUltimoCosto.TabIndex = 6
    '
    'lbScostamento
    '
    Me.lbScostamento.AutoSize = True
    Me.lbScostamento.BackColor = System.Drawing.Color.Transparent
    Me.lbScostamento.Location = New System.Drawing.Point(476, 38)
    Me.lbScostamento.Name = "lbScostamento"
    Me.lbScostamento.NTSDbField = ""
    Me.lbScostamento.Size = New System.Drawing.Size(69, 13)
    Me.lbScostamento.TabIndex = 7
    Me.lbScostamento.Text = "Scostamento"
    Me.lbScostamento.Tooltip = ""
    Me.lbScostamento.UseMnemonic = False
    '
    'edScostamento
    '
    Me.edScostamento.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScostamento.Enabled = False
    Me.edScostamento.Location = New System.Drawing.Point(550, 35)
    Me.edScostamento.Name = "edScostamento"
    Me.edScostamento.NTSDbField = ""
    Me.edScostamento.NTSFormat = "0"
    Me.edScostamento.NTSForzaVisZoom = False
    Me.edScostamento.NTSOldValue = ""
    Me.edScostamento.Properties.Appearance.Options.UseTextOptions = True
    Me.edScostamento.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edScostamento.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScostamento.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScostamento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScostamento.Properties.MaxLength = 65536
    Me.edScostamento.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScostamento.Size = New System.Drawing.Size(62, 20)
    Me.edScostamento.TabIndex = 8
    '
    'lbLabel1
    '
    Me.lbLabel1.AutoSize = True
    Me.lbLabel1.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel1.Location = New System.Drawing.Point(618, 38)
    Me.lbLabel1.Name = "lbLabel1"
    Me.lbLabel1.NTSDbField = ""
    Me.lbLabel1.Size = New System.Drawing.Size(18, 13)
    Me.lbLabel1.TabIndex = 9
    Me.lbLabel1.Text = "%"
    Me.lbLabel1.Tooltip = ""
    Me.lbLabel1.UseMnemonic = False
    '
    'lbDataEffetto
    '
    Me.lbDataEffetto.AutoSize = True
    Me.lbDataEffetto.BackColor = System.Drawing.Color.Transparent
    Me.lbDataEffetto.Location = New System.Drawing.Point(13, 61)
    Me.lbDataEffetto.Name = "lbDataEffetto"
    Me.lbDataEffetto.NTSDbField = ""
    Me.lbDataEffetto.Size = New System.Drawing.Size(67, 13)
    Me.lbDataEffetto.TabIndex = 10
    Me.lbDataEffetto.Text = "Data Effetto"
    Me.lbDataEffetto.Tooltip = ""
    Me.lbDataEffetto.UseMnemonic = False
    '
    'edDataEffetto
    '
    Me.edDataEffetto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataEffetto.Location = New System.Drawing.Point(121, 58)
    Me.edDataEffetto.Name = "edDataEffetto"
    Me.edDataEffetto.NTSDbField = ""
    Me.edDataEffetto.NTSForzaVisZoom = False
    Me.edDataEffetto.NTSOldValue = ""
    Me.edDataEffetto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataEffetto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataEffetto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataEffetto.Properties.MaxLength = 65536
    Me.edDataEffetto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataEffetto.Size = New System.Drawing.Size(100, 20)
    Me.edDataEffetto.TabIndex = 11
    '
    'lbQuantIni
    '
    Me.lbQuantIni.AutoSize = True
    Me.lbQuantIni.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantIni.Location = New System.Drawing.Point(284, 61)
    Me.lbQuantIni.Name = "lbQuantIni"
    Me.lbQuantIni.NTSDbField = ""
    Me.lbQuantIni.Size = New System.Drawing.Size(63, 13)
    Me.lbQuantIni.TabIndex = 12
    Me.lbQuantIni.Text = "Da quantità"
    Me.lbQuantIni.Tooltip = ""
    Me.lbQuantIni.UseMnemonic = False
    '
    'edQuantIni
    '
    Me.edQuantIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantIni.Location = New System.Drawing.Point(379, 58)
    Me.edQuantIni.Name = "edQuantIni"
    Me.edQuantIni.NTSDbField = ""
    Me.edQuantIni.NTSFormat = "0"
    Me.edQuantIni.NTSForzaVisZoom = False
    Me.edQuantIni.NTSOldValue = ""
    Me.edQuantIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantIni.Properties.MaxLength = 65536
    Me.edQuantIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantIni.Size = New System.Drawing.Size(86, 20)
    Me.edQuantIni.TabIndex = 13
    '
    'lbQuantFin
    '
    Me.lbQuantFin.AutoSize = True
    Me.lbQuantFin.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantFin.Location = New System.Drawing.Point(476, 61)
    Me.lbQuantFin.Name = "lbQuantFin"
    Me.lbQuantFin.NTSDbField = ""
    Me.lbQuantFin.Size = New System.Drawing.Size(57, 13)
    Me.lbQuantFin.TabIndex = 14
    Me.lbQuantFin.Text = "A quantità"
    Me.lbQuantFin.Tooltip = ""
    Me.lbQuantFin.UseMnemonic = False
    '
    'edQuantFin
    '
    Me.edQuantFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantFin.Location = New System.Drawing.Point(550, 58)
    Me.edQuantFin.Name = "edQuantFin"
    Me.edQuantFin.NTSDbField = ""
    Me.edQuantFin.NTSFormat = "0"
    Me.edQuantFin.NTSForzaVisZoom = False
    Me.edQuantFin.NTSOldValue = ""
    Me.edQuantFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantFin.Properties.MaxLength = 65536
    Me.edQuantFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantFin.Size = New System.Drawing.Size(86, 20)
    Me.edQuantFin.TabIndex = 15
    '
    'lbRicMar
    '
    Me.lbRicMar.AutoSize = True
    Me.lbRicMar.BackColor = System.Drawing.Color.Transparent
    Me.lbRicMar.Location = New System.Drawing.Point(13, 84)
    Me.lbRicMar.Name = "lbRicMar"
    Me.lbRicMar.NTSDbField = ""
    Me.lbRicMar.Size = New System.Drawing.Size(90, 13)
    Me.lbRicMar.TabIndex = 16
    Me.lbRicMar.Text = "Ricalcolo/Margine"
    Me.lbRicMar.Tooltip = ""
    Me.lbRicMar.UseMnemonic = False
    '
    'lbArrotondamento
    '
    Me.lbArrotondamento.AutoSize = True
    Me.lbArrotondamento.BackColor = System.Drawing.Color.Transparent
    Me.lbArrotondamento.Location = New System.Drawing.Point(284, 84)
    Me.lbArrotondamento.Name = "lbArrotondamento"
    Me.lbArrotondamento.NTSDbField = ""
    Me.lbArrotondamento.Size = New System.Drawing.Size(86, 13)
    Me.lbArrotondamento.TabIndex = 18
    Me.lbArrotondamento.Text = "Arrotondamento"
    Me.lbArrotondamento.Tooltip = ""
    Me.lbArrotondamento.UseMnemonic = False
    '
    'edArrotondamento
    '
    Me.edArrotondamento.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArrotondamento.Location = New System.Drawing.Point(379, 81)
    Me.edArrotondamento.Name = "edArrotondamento"
    Me.edArrotondamento.NTSDbField = ""
    Me.edArrotondamento.NTSFormat = "0"
    Me.edArrotondamento.NTSForzaVisZoom = False
    Me.edArrotondamento.NTSOldValue = ""
    Me.edArrotondamento.Properties.Appearance.Options.UseTextOptions = True
    Me.edArrotondamento.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArrotondamento.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArrotondamento.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArrotondamento.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArrotondamento.Properties.MaxLength = 65536
    Me.edArrotondamento.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArrotondamento.Size = New System.Drawing.Size(86, 20)
    Me.edArrotondamento.TabIndex = 19
    '
    'lbPercRicMar
    '
    Me.lbPercRicMar.AutoSize = True
    Me.lbPercRicMar.BackColor = System.Drawing.Color.Transparent
    Me.lbPercRicMar.Location = New System.Drawing.Point(13, 108)
    Me.lbPercRicMar.Name = "lbPercRicMar"
    Me.lbPercRicMar.NTSDbField = ""
    Me.lbPercRicMar.Size = New System.Drawing.Size(100, 13)
    Me.lbPercRicMar.TabIndex = 20
    Me.lbPercRicMar.Text = "% Margine/Ricarico"
    Me.lbPercRicMar.Tooltip = ""
    Me.lbPercRicMar.UseMnemonic = False
    '
    'edPerc1
    '
    Me.edPerc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPerc1.Location = New System.Drawing.Point(121, 105)
    Me.edPerc1.Name = "edPerc1"
    Me.edPerc1.NTSDbField = ""
    Me.edPerc1.NTSFormat = "0"
    Me.edPerc1.NTSForzaVisZoom = False
    Me.edPerc1.NTSOldValue = ""
    Me.edPerc1.Properties.Appearance.Options.UseTextOptions = True
    Me.edPerc1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPerc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPerc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPerc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPerc1.Properties.MaxLength = 65536
    Me.edPerc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPerc1.Size = New System.Drawing.Size(76, 20)
    Me.edPerc1.TabIndex = 21
    '
    'edPerc2
    '
    Me.edPerc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPerc2.Location = New System.Drawing.Point(198, 105)
    Me.edPerc2.Name = "edPerc2"
    Me.edPerc2.NTSDbField = ""
    Me.edPerc2.NTSFormat = "0"
    Me.edPerc2.NTSForzaVisZoom = False
    Me.edPerc2.NTSOldValue = ""
    Me.edPerc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edPerc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPerc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPerc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPerc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPerc2.Properties.MaxLength = 65536
    Me.edPerc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPerc2.Size = New System.Drawing.Size(73, 20)
    Me.edPerc2.TabIndex = 22
    '
    'lbRelazioneListini
    '
    Me.lbRelazioneListini.AutoSize = True
    Me.lbRelazioneListini.BackColor = System.Drawing.Color.Transparent
    Me.lbRelazioneListini.Location = New System.Drawing.Point(284, 108)
    Me.lbRelazioneListini.Name = "lbRelazioneListini"
    Me.lbRelazioneListini.NTSDbField = ""
    Me.lbRelazioneListini.Size = New System.Drawing.Size(82, 13)
    Me.lbRelazioneListini.TabIndex = 23
    Me.lbRelazioneListini.Text = "Relazione Listini"
    Me.lbRelazioneListini.Tooltip = ""
    Me.lbRelazioneListini.UseMnemonic = False
    '
    'edRelazioneListini
    '
    Me.edRelazioneListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRelazioneListini.Enabled = False
    Me.edRelazioneListini.Location = New System.Drawing.Point(379, 105)
    Me.edRelazioneListini.Name = "edRelazioneListini"
    Me.edRelazioneListini.NTSDbField = ""
    Me.edRelazioneListini.NTSForzaVisZoom = False
    Me.edRelazioneListini.NTSOldValue = ""
    Me.edRelazioneListini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRelazioneListini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRelazioneListini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRelazioneListini.Properties.MaxLength = 65536
    Me.edRelazioneListini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRelazioneListini.Size = New System.Drawing.Size(257, 20)
    Me.edRelazioneListini.TabIndex = 24
    '
    'fmListini
    '
    Me.fmListini.AllowDrop = True
    Me.fmListini.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmListini.Appearance.Options.UseBackColor = True
    Me.fmListini.Controls.Add(Me.edDataPrec8)
    Me.fmListini.Controls.Add(Me.edDataPrec7)
    Me.fmListini.Controls.Add(Me.edDataPrec6)
    Me.fmListini.Controls.Add(Me.edDataPrec5)
    Me.fmListini.Controls.Add(Me.edDataPrec4)
    Me.fmListini.Controls.Add(Me.edDataPrec3)
    Me.fmListini.Controls.Add(Me.edDataPrec2)
    Me.fmListini.Controls.Add(Me.edDataPrec1)
    Me.fmListini.Controls.Add(Me.edPrezzoOld8)
    Me.fmListini.Controls.Add(Me.edPrezzoOld7)
    Me.fmListini.Controls.Add(Me.edPrezzoOld6)
    Me.fmListini.Controls.Add(Me.edPrezzoOld5)
    Me.fmListini.Controls.Add(Me.edPrezzoOld4)
    Me.fmListini.Controls.Add(Me.edPrezzoOld3)
    Me.fmListini.Controls.Add(Me.edPrezzoOld2)
    Me.fmListini.Controls.Add(Me.edPrezzoOld1)
    Me.fmListini.Controls.Add(Me.edPrezzoNew8)
    Me.fmListini.Controls.Add(Me.edPrezzoNew7)
    Me.fmListini.Controls.Add(Me.edPrezzoNew6)
    Me.fmListini.Controls.Add(Me.edPrezzoNew5)
    Me.fmListini.Controls.Add(Me.edPrezzoNew4)
    Me.fmListini.Controls.Add(Me.edPrezzoNew3)
    Me.fmListini.Controls.Add(Me.edPrezzoNew2)
    Me.fmListini.Controls.Add(Me.edPrezzoNew1)
    Me.fmListini.Controls.Add(Me.lbLabel13)
    Me.fmListini.Controls.Add(Me.lbLabel12)
    Me.fmListini.Controls.Add(Me.lbLabel11)
    Me.fmListini.Controls.Add(Me.lbLabel10)
    Me.fmListini.Controls.Add(Me.lbLabel9)
    Me.fmListini.Controls.Add(Me.lbLabel8)
    Me.fmListini.Controls.Add(Me.lbLabel7)
    Me.fmListini.Controls.Add(Me.lbLabel6)
    Me.fmListini.Controls.Add(Me.lbLabel5)
    Me.fmListini.Controls.Add(Me.lbLabel4)
    Me.fmListini.Controls.Add(Me.lbLabel3)
    Me.fmListini.Controls.Add(Me.lbLabel2)
    Me.fmListini.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmListini.Location = New System.Drawing.Point(16, 140)
    Me.fmListini.Name = "fmListini"
    Me.fmListini.Size = New System.Drawing.Size(371, 232)
    Me.fmListini.TabIndex = 25
    Me.fmListini.Text = "Listini:"
    '
    'edDataPrec8
    '
    Me.edDataPrec8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec8.Enabled = False
    Me.edDataPrec8.Location = New System.Drawing.Point(244, 199)
    Me.edDataPrec8.Name = "edDataPrec8"
    Me.edDataPrec8.NTSDbField = ""
    Me.edDataPrec8.NTSForzaVisZoom = False
    Me.edDataPrec8.NTSOldValue = ""
    Me.edDataPrec8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec8.Properties.MaxLength = 65536
    Me.edDataPrec8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec8.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec8.TabIndex = 35
    '
    'edDataPrec7
    '
    Me.edDataPrec7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec7.Enabled = False
    Me.edDataPrec7.Location = New System.Drawing.Point(244, 177)
    Me.edDataPrec7.Name = "edDataPrec7"
    Me.edDataPrec7.NTSDbField = ""
    Me.edDataPrec7.NTSForzaVisZoom = False
    Me.edDataPrec7.NTSOldValue = ""
    Me.edDataPrec7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec7.Properties.MaxLength = 65536
    Me.edDataPrec7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec7.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec7.TabIndex = 34
    '
    'edDataPrec6
    '
    Me.edDataPrec6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec6.Enabled = False
    Me.edDataPrec6.Location = New System.Drawing.Point(244, 155)
    Me.edDataPrec6.Name = "edDataPrec6"
    Me.edDataPrec6.NTSDbField = ""
    Me.edDataPrec6.NTSForzaVisZoom = False
    Me.edDataPrec6.NTSOldValue = ""
    Me.edDataPrec6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec6.Properties.MaxLength = 65536
    Me.edDataPrec6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec6.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec6.TabIndex = 33
    '
    'edDataPrec5
    '
    Me.edDataPrec5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec5.Enabled = False
    Me.edDataPrec5.Location = New System.Drawing.Point(244, 133)
    Me.edDataPrec5.Name = "edDataPrec5"
    Me.edDataPrec5.NTSDbField = ""
    Me.edDataPrec5.NTSForzaVisZoom = False
    Me.edDataPrec5.NTSOldValue = ""
    Me.edDataPrec5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec5.Properties.MaxLength = 65536
    Me.edDataPrec5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec5.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec5.TabIndex = 32
    '
    'edDataPrec4
    '
    Me.edDataPrec4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec4.Enabled = False
    Me.edDataPrec4.Location = New System.Drawing.Point(244, 111)
    Me.edDataPrec4.Name = "edDataPrec4"
    Me.edDataPrec4.NTSDbField = ""
    Me.edDataPrec4.NTSForzaVisZoom = False
    Me.edDataPrec4.NTSOldValue = ""
    Me.edDataPrec4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec4.Properties.MaxLength = 65536
    Me.edDataPrec4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec4.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec4.TabIndex = 31
    '
    'edDataPrec3
    '
    Me.edDataPrec3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec3.Enabled = False
    Me.edDataPrec3.Location = New System.Drawing.Point(244, 89)
    Me.edDataPrec3.Name = "edDataPrec3"
    Me.edDataPrec3.NTSDbField = ""
    Me.edDataPrec3.NTSForzaVisZoom = False
    Me.edDataPrec3.NTSOldValue = ""
    Me.edDataPrec3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec3.Properties.MaxLength = 65536
    Me.edDataPrec3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec3.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec3.TabIndex = 30
    '
    'edDataPrec2
    '
    Me.edDataPrec2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec2.Enabled = False
    Me.edDataPrec2.Location = New System.Drawing.Point(244, 67)
    Me.edDataPrec2.Name = "edDataPrec2"
    Me.edDataPrec2.NTSDbField = ""
    Me.edDataPrec2.NTSForzaVisZoom = False
    Me.edDataPrec2.NTSOldValue = ""
    Me.edDataPrec2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec2.Properties.MaxLength = 65536
    Me.edDataPrec2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec2.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec2.TabIndex = 29
    '
    'edDataPrec1
    '
    Me.edDataPrec1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataPrec1.Enabled = False
    Me.edDataPrec1.Location = New System.Drawing.Point(244, 45)
    Me.edDataPrec1.Name = "edDataPrec1"
    Me.edDataPrec1.NTSDbField = ""
    Me.edDataPrec1.NTSForzaVisZoom = False
    Me.edDataPrec1.NTSOldValue = ""
    Me.edDataPrec1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataPrec1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataPrec1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataPrec1.Properties.MaxLength = 65536
    Me.edDataPrec1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataPrec1.Size = New System.Drawing.Size(100, 20)
    Me.edDataPrec1.TabIndex = 28
    '
    'edPrezzoOld8
    '
    Me.edPrezzoOld8.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld8.Enabled = False
    Me.edPrezzoOld8.Location = New System.Drawing.Point(155, 199)
    Me.edPrezzoOld8.Name = "edPrezzoOld8"
    Me.edPrezzoOld8.NTSDbField = ""
    Me.edPrezzoOld8.NTSFormat = "0"
    Me.edPrezzoOld8.NTSForzaVisZoom = False
    Me.edPrezzoOld8.NTSOldValue = ""
    Me.edPrezzoOld8.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld8.Properties.MaxLength = 65536
    Me.edPrezzoOld8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld8.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld8.TabIndex = 27
    '
    'edPrezzoOld7
    '
    Me.edPrezzoOld7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld7.Enabled = False
    Me.edPrezzoOld7.Location = New System.Drawing.Point(155, 177)
    Me.edPrezzoOld7.Name = "edPrezzoOld7"
    Me.edPrezzoOld7.NTSDbField = ""
    Me.edPrezzoOld7.NTSFormat = "0"
    Me.edPrezzoOld7.NTSForzaVisZoom = False
    Me.edPrezzoOld7.NTSOldValue = ""
    Me.edPrezzoOld7.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld7.Properties.MaxLength = 65536
    Me.edPrezzoOld7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld7.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld7.TabIndex = 26
    '
    'edPrezzoOld6
    '
    Me.edPrezzoOld6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld6.Enabled = False
    Me.edPrezzoOld6.Location = New System.Drawing.Point(156, 155)
    Me.edPrezzoOld6.Name = "edPrezzoOld6"
    Me.edPrezzoOld6.NTSDbField = ""
    Me.edPrezzoOld6.NTSFormat = "0"
    Me.edPrezzoOld6.NTSForzaVisZoom = False
    Me.edPrezzoOld6.NTSOldValue = ""
    Me.edPrezzoOld6.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld6.Properties.MaxLength = 65536
    Me.edPrezzoOld6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld6.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld6.TabIndex = 25
    '
    'edPrezzoOld5
    '
    Me.edPrezzoOld5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld5.Enabled = False
    Me.edPrezzoOld5.Location = New System.Drawing.Point(156, 133)
    Me.edPrezzoOld5.Name = "edPrezzoOld5"
    Me.edPrezzoOld5.NTSDbField = ""
    Me.edPrezzoOld5.NTSFormat = "0"
    Me.edPrezzoOld5.NTSForzaVisZoom = False
    Me.edPrezzoOld5.NTSOldValue = ""
    Me.edPrezzoOld5.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld5.Properties.MaxLength = 65536
    Me.edPrezzoOld5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld5.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld5.TabIndex = 24
    '
    'edPrezzoOld4
    '
    Me.edPrezzoOld4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld4.Enabled = False
    Me.edPrezzoOld4.Location = New System.Drawing.Point(156, 111)
    Me.edPrezzoOld4.Name = "edPrezzoOld4"
    Me.edPrezzoOld4.NTSDbField = ""
    Me.edPrezzoOld4.NTSFormat = "0"
    Me.edPrezzoOld4.NTSForzaVisZoom = False
    Me.edPrezzoOld4.NTSOldValue = ""
    Me.edPrezzoOld4.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld4.Properties.MaxLength = 65536
    Me.edPrezzoOld4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld4.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld4.TabIndex = 23
    '
    'edPrezzoOld3
    '
    Me.edPrezzoOld3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld3.Enabled = False
    Me.edPrezzoOld3.Location = New System.Drawing.Point(156, 89)
    Me.edPrezzoOld3.Name = "edPrezzoOld3"
    Me.edPrezzoOld3.NTSDbField = ""
    Me.edPrezzoOld3.NTSFormat = "0"
    Me.edPrezzoOld3.NTSForzaVisZoom = False
    Me.edPrezzoOld3.NTSOldValue = ""
    Me.edPrezzoOld3.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld3.Properties.MaxLength = 65536
    Me.edPrezzoOld3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld3.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld3.TabIndex = 22
    '
    'edPrezzoOld2
    '
    Me.edPrezzoOld2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld2.Enabled = False
    Me.edPrezzoOld2.Location = New System.Drawing.Point(156, 67)
    Me.edPrezzoOld2.Name = "edPrezzoOld2"
    Me.edPrezzoOld2.NTSDbField = ""
    Me.edPrezzoOld2.NTSFormat = "0"
    Me.edPrezzoOld2.NTSForzaVisZoom = False
    Me.edPrezzoOld2.NTSOldValue = ""
    Me.edPrezzoOld2.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld2.Properties.MaxLength = 65536
    Me.edPrezzoOld2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld2.Size = New System.Drawing.Size(80, 20)
    Me.edPrezzoOld2.TabIndex = 21
    '
    'edPrezzoOld1
    '
    Me.edPrezzoOld1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoOld1.Enabled = False
    Me.edPrezzoOld1.Location = New System.Drawing.Point(156, 45)
    Me.edPrezzoOld1.Name = "edPrezzoOld1"
    Me.edPrezzoOld1.NTSDbField = ""
    Me.edPrezzoOld1.NTSFormat = "0"
    Me.edPrezzoOld1.NTSForzaVisZoom = False
    Me.edPrezzoOld1.NTSOldValue = ""
    Me.edPrezzoOld1.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoOld1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoOld1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoOld1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoOld1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoOld1.Properties.MaxLength = 65536
    Me.edPrezzoOld1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoOld1.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoOld1.TabIndex = 20
    '
    'edPrezzoNew8
    '
    Me.edPrezzoNew8.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPrezzoNew8.Location = New System.Drawing.Point(62, 199)
    Me.edPrezzoNew8.Name = "edPrezzoNew8"
    Me.edPrezzoNew8.NTSDbField = ""
    Me.edPrezzoNew8.NTSFormat = "0"
    Me.edPrezzoNew8.NTSForzaVisZoom = False
    Me.edPrezzoNew8.NTSOldValue = ""
    Me.edPrezzoNew8.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew8.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew8.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew8.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew8.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew8.Properties.MaxLength = 65536
    Me.edPrezzoNew8.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew8.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew8.TabIndex = 19
    '
    'edPrezzoNew7
    '
    Me.edPrezzoNew7.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoNew7.Location = New System.Drawing.Point(62, 177)
    Me.edPrezzoNew7.Name = "edPrezzoNew7"
    Me.edPrezzoNew7.NTSDbField = ""
    Me.edPrezzoNew7.NTSFormat = "0"
    Me.edPrezzoNew7.NTSForzaVisZoom = False
    Me.edPrezzoNew7.NTSOldValue = ""
    Me.edPrezzoNew7.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew7.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew7.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew7.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew7.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew7.Properties.MaxLength = 65536
    Me.edPrezzoNew7.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew7.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew7.TabIndex = 18
    '
    'edPrezzoNew6
    '
    Me.edPrezzoNew6.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPrezzoNew6.Location = New System.Drawing.Point(62, 155)
    Me.edPrezzoNew6.Name = "edPrezzoNew6"
    Me.edPrezzoNew6.NTSDbField = ""
    Me.edPrezzoNew6.NTSFormat = "0"
    Me.edPrezzoNew6.NTSForzaVisZoom = False
    Me.edPrezzoNew6.NTSOldValue = ""
    Me.edPrezzoNew6.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew6.Properties.MaxLength = 65536
    Me.edPrezzoNew6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew6.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew6.TabIndex = 17
    '
    'edPrezzoNew5
    '
    Me.edPrezzoNew5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoNew5.Location = New System.Drawing.Point(62, 133)
    Me.edPrezzoNew5.Name = "edPrezzoNew5"
    Me.edPrezzoNew5.NTSDbField = ""
    Me.edPrezzoNew5.NTSFormat = "0"
    Me.edPrezzoNew5.NTSForzaVisZoom = False
    Me.edPrezzoNew5.NTSOldValue = ""
    Me.edPrezzoNew5.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew5.Properties.MaxLength = 65536
    Me.edPrezzoNew5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew5.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew5.TabIndex = 16
    '
    'edPrezzoNew4
    '
    Me.edPrezzoNew4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoNew4.Location = New System.Drawing.Point(62, 111)
    Me.edPrezzoNew4.Name = "edPrezzoNew4"
    Me.edPrezzoNew4.NTSDbField = ""
    Me.edPrezzoNew4.NTSFormat = "0"
    Me.edPrezzoNew4.NTSForzaVisZoom = False
    Me.edPrezzoNew4.NTSOldValue = ""
    Me.edPrezzoNew4.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew4.Properties.MaxLength = 65536
    Me.edPrezzoNew4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew4.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew4.TabIndex = 15
    '
    'edPrezzoNew3
    '
    Me.edPrezzoNew3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoNew3.Location = New System.Drawing.Point(62, 89)
    Me.edPrezzoNew3.Name = "edPrezzoNew3"
    Me.edPrezzoNew3.NTSDbField = ""
    Me.edPrezzoNew3.NTSFormat = "0"
    Me.edPrezzoNew3.NTSForzaVisZoom = False
    Me.edPrezzoNew3.NTSOldValue = ""
    Me.edPrezzoNew3.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew3.Properties.MaxLength = 65536
    Me.edPrezzoNew3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew3.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew3.TabIndex = 14
    '
    'edPrezzoNew2
    '
    Me.edPrezzoNew2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoNew2.Location = New System.Drawing.Point(62, 67)
    Me.edPrezzoNew2.Name = "edPrezzoNew2"
    Me.edPrezzoNew2.NTSDbField = ""
    Me.edPrezzoNew2.NTSFormat = "0"
    Me.edPrezzoNew2.NTSForzaVisZoom = False
    Me.edPrezzoNew2.NTSOldValue = ""
    Me.edPrezzoNew2.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew2.Properties.MaxLength = 65536
    Me.edPrezzoNew2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew2.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew2.TabIndex = 13
    '
    'edPrezzoNew1
    '
    Me.edPrezzoNew1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPrezzoNew1.Location = New System.Drawing.Point(62, 45)
    Me.edPrezzoNew1.Name = "edPrezzoNew1"
    Me.edPrezzoNew1.NTSDbField = ""
    Me.edPrezzoNew1.NTSFormat = "0"
    Me.edPrezzoNew1.NTSForzaVisZoom = False
    Me.edPrezzoNew1.NTSOldValue = ""
    Me.edPrezzoNew1.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzoNew1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzoNew1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzoNew1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzoNew1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzoNew1.Properties.MaxLength = 65536
    Me.edPrezzoNew1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzoNew1.Size = New System.Drawing.Size(79, 20)
    Me.edPrezzoNew1.TabIndex = 12
    '
    'lbLabel13
    '
    Me.lbLabel13.AutoSize = True
    Me.lbLabel13.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel13.Location = New System.Drawing.Point(20, 202)
    Me.lbLabel13.Name = "lbLabel13"
    Me.lbLabel13.NTSDbField = ""
    Me.lbLabel13.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel13.TabIndex = 11
    Me.lbLabel13.Text = "8]"
    Me.lbLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel13.Tooltip = ""
    Me.lbLabel13.UseMnemonic = False
    '
    'lbLabel12
    '
    Me.lbLabel12.AutoSize = True
    Me.lbLabel12.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel12.Location = New System.Drawing.Point(20, 180)
    Me.lbLabel12.Name = "lbLabel12"
    Me.lbLabel12.NTSDbField = ""
    Me.lbLabel12.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel12.TabIndex = 10
    Me.lbLabel12.Text = "7]"
    Me.lbLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel12.Tooltip = ""
    Me.lbLabel12.UseMnemonic = False
    '
    'lbLabel11
    '
    Me.lbLabel11.AutoSize = True
    Me.lbLabel11.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel11.Location = New System.Drawing.Point(20, 158)
    Me.lbLabel11.Name = "lbLabel11"
    Me.lbLabel11.NTSDbField = ""
    Me.lbLabel11.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel11.TabIndex = 9
    Me.lbLabel11.Text = "6]"
    Me.lbLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel11.Tooltip = ""
    Me.lbLabel11.UseMnemonic = False
    '
    'lbLabel10
    '
    Me.lbLabel10.AutoSize = True
    Me.lbLabel10.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel10.Location = New System.Drawing.Point(20, 136)
    Me.lbLabel10.Name = "lbLabel10"
    Me.lbLabel10.NTSDbField = ""
    Me.lbLabel10.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel10.TabIndex = 8
    Me.lbLabel10.Text = "5]"
    Me.lbLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel10.Tooltip = ""
    Me.lbLabel10.UseMnemonic = False
    '
    'lbLabel9
    '
    Me.lbLabel9.AutoSize = True
    Me.lbLabel9.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel9.Location = New System.Drawing.Point(20, 114)
    Me.lbLabel9.Name = "lbLabel9"
    Me.lbLabel9.NTSDbField = ""
    Me.lbLabel9.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel9.TabIndex = 7
    Me.lbLabel9.Text = "4]"
    Me.lbLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel9.Tooltip = ""
    Me.lbLabel9.UseMnemonic = False
    '
    'lbLabel8
    '
    Me.lbLabel8.AutoSize = True
    Me.lbLabel8.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel8.Location = New System.Drawing.Point(20, 92)
    Me.lbLabel8.Name = "lbLabel8"
    Me.lbLabel8.NTSDbField = ""
    Me.lbLabel8.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel8.TabIndex = 6
    Me.lbLabel8.Text = "3]"
    Me.lbLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel8.Tooltip = ""
    Me.lbLabel8.UseMnemonic = False
    '
    'lbLabel7
    '
    Me.lbLabel7.AutoSize = True
    Me.lbLabel7.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel7.Location = New System.Drawing.Point(20, 70)
    Me.lbLabel7.Name = "lbLabel7"
    Me.lbLabel7.NTSDbField = ""
    Me.lbLabel7.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel7.TabIndex = 5
    Me.lbLabel7.Text = "2]"
    Me.lbLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel7.Tooltip = ""
    Me.lbLabel7.UseMnemonic = False
    '
    'lbLabel6
    '
    Me.lbLabel6.AutoSize = True
    Me.lbLabel6.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel6.Location = New System.Drawing.Point(20, 48)
    Me.lbLabel6.Name = "lbLabel6"
    Me.lbLabel6.NTSDbField = ""
    Me.lbLabel6.Size = New System.Drawing.Size(17, 13)
    Me.lbLabel6.TabIndex = 4
    Me.lbLabel6.Text = "1]"
    Me.lbLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel6.Tooltip = ""
    Me.lbLabel6.UseMnemonic = False
    '
    'lbLabel5
    '
    Me.lbLabel5.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel5.Location = New System.Drawing.Point(241, 27)
    Me.lbLabel5.Name = "lbLabel5"
    Me.lbLabel5.NTSDbField = ""
    Me.lbLabel5.Size = New System.Drawing.Size(107, 13)
    Me.lbLabel5.TabIndex = 3
    Me.lbLabel5.Text = "Data Precedente"
    Me.lbLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel5.Tooltip = ""
    Me.lbLabel5.UseMnemonic = False
    '
    'lbLabel4
    '
    Me.lbLabel4.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel4.Location = New System.Drawing.Point(147, 25)
    Me.lbLabel4.Name = "lbLabel4"
    Me.lbLabel4.NTSDbField = ""
    Me.lbLabel4.Size = New System.Drawing.Size(99, 17)
    Me.lbLabel4.TabIndex = 2
    Me.lbLabel4.Text = "Vecchio Prezzo"
    Me.lbLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel4.Tooltip = ""
    Me.lbLabel4.UseMnemonic = False
    '
    'lbLabel3
    '
    Me.lbLabel3.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel3.Location = New System.Drawing.Point(62, 25)
    Me.lbLabel3.Name = "lbLabel3"
    Me.lbLabel3.NTSDbField = ""
    Me.lbLabel3.Size = New System.Drawing.Size(79, 13)
    Me.lbLabel3.TabIndex = 1
    Me.lbLabel3.Text = "Prezzo"
    Me.lbLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel3.Tooltip = ""
    Me.lbLabel3.UseMnemonic = False
    '
    'lbLabel2
    '
    Me.lbLabel2.BackColor = System.Drawing.Color.Transparent
    Me.lbLabel2.Location = New System.Drawing.Point(5, 22)
    Me.lbLabel2.Name = "lbLabel2"
    Me.lbLabel2.NTSDbField = ""
    Me.lbLabel2.Size = New System.Drawing.Size(57, 18)
    Me.lbLabel2.TabIndex = 0
    Me.lbLabel2.Text = "N° Listino"
    Me.lbLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    Me.lbLabel2.Tooltip = ""
    Me.lbLabel2.UseMnemonic = False
    '
    'cmdRicalcola
    '
    Me.cmdRicalcola.ImageText = ""
    Me.cmdRicalcola.Location = New System.Drawing.Point(3, 3)
    Me.cmdRicalcola.Name = "cmdRicalcola"
    Me.cmdRicalcola.Size = New System.Drawing.Size(171, 23)
    Me.cmdRicalcola.TabIndex = 26
    Me.cmdRicalcola.Text = "Ricalcola Prezzi"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 28)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(171, 23)
    Me.cmdOk.TabIndex = 27
    Me.cmdOk.Text = "Aggiorna Prezzi"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 67)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(171, 23)
    Me.cmdAnnulla.TabIndex = 28
    Me.cmdAnnulla.Text = "Esci"
    '
    'pnPanel1
    '
    Me.pnPanel1.AllowDrop = True
    Me.pnPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPanel1.Appearance.Options.UseBackColor = True
    Me.pnPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPanel1.Controls.Add(Me.cmdRicalcola)
    Me.pnPanel1.Controls.Add(Me.cmdAnnulla)
    Me.pnPanel1.Controls.Add(Me.cmdOk)
    Me.pnPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPanel1.Location = New System.Drawing.Point(467, 268)
    Me.pnPanel1.Name = "pnPanel1"
    Me.pnPanel1.Size = New System.Drawing.Size(178, 104)
    Me.pnPanel1.TabIndex = 29
    Me.pnPanel1.Text = "NtsPanel1"
    '
    'edRicMar
    '
    Me.edRicMar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRicMar.Enabled = False
    Me.edRicMar.Location = New System.Drawing.Point(121, 81)
    Me.edRicMar.Name = "edRicMar"
    Me.edRicMar.NTSDbField = ""
    Me.edRicMar.NTSForzaVisZoom = False
    Me.edRicMar.NTSOldValue = ""
    Me.edRicMar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicMar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicMar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicMar.Properties.MaxLength = 65536
    Me.edRicMar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicMar.Size = New System.Drawing.Size(100, 20)
    Me.edRicMar.TabIndex = 30
    '
    'FRMMGRCPR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(654, 385)
    Me.Controls.Add(Me.edRicMar)
    Me.Controls.Add(Me.pnPanel1)
    Me.Controls.Add(Me.fmListini)
    Me.Controls.Add(Me.edRelazioneListini)
    Me.Controls.Add(Me.lbRelazioneListini)
    Me.Controls.Add(Me.edPerc2)
    Me.Controls.Add(Me.edPerc1)
    Me.Controls.Add(Me.lbPercRicMar)
    Me.Controls.Add(Me.edArrotondamento)
    Me.Controls.Add(Me.lbArrotondamento)
    Me.Controls.Add(Me.lbRicMar)
    Me.Controls.Add(Me.edQuantFin)
    Me.Controls.Add(Me.lbQuantFin)
    Me.Controls.Add(Me.edQuantIni)
    Me.Controls.Add(Me.lbQuantIni)
    Me.Controls.Add(Me.edDataEffetto)
    Me.Controls.Add(Me.lbDataEffetto)
    Me.Controls.Add(Me.lbLabel1)
    Me.Controls.Add(Me.edScostamento)
    Me.Controls.Add(Me.lbScostamento)
    Me.Controls.Add(Me.edUltimoCosto)
    Me.Controls.Add(Me.lbUltimoCosto)
    Me.Controls.Add(Me.edPrezzoBase)
    Me.Controls.Add(Me.lbPrezzoBase)
    Me.Controls.Add(Me.lbDescodart)
    Me.Controls.Add(Me.edCodart)
    Me.Controls.Add(Me.lbCodart)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGRCPR"
    Me.Text = "RICALCOLO PREZZI"
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoBase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUltimoCosto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edScostamento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataEffetto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuantIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuantFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArrotondamento.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPerc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPerc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRelazioneListini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmListini, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmListini.ResumeLayout(False)
    Me.fmListini.PerformLayout()
    CType(Me.edDataPrec8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataPrec1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoOld1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPrezzoNew1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPanel1.ResumeLayout(False)
    CType(Me.edRicMar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '----------------------------------------------------------------------------------
      edPrezzoBase.NTSSetParam(oMenu, oApp.Tr(Me, 128578182280427786, "Prezzo Base"), oApp.FormatPrzUn)
      edUltimoCosto.NTSSetParam(oMenu, oApp.Tr(Me, 128578214055127303, "Ultimo Costo"), oApp.FormatPrzUn)
      edScostamento.NTSSetParam(oMenu, oApp.Tr(Me, 128578214341849723, "Scostamento"), oApp.FormatSconti)
      edDataEffetto.NTSSetParam(oMenu, oApp.Tr(Me, 128580987036258138, "Data Effetto"), False)
      edQuantIni.NTSSetParam(oMenu, oApp.Tr(Me, 128578214919825871, "Da quantità"), oApp.FormatImporti)
      edQuantFin.NTSSetParam(oMenu, oApp.Tr(Me, 128578215439520023, "A quantità"), oApp.FormatImporti)
      edRicMar.NTSSetParam(oMenu, oApp.Tr(Me, 128580991522440074, "Ricalcolo/Margine"), 0)
      edArrotondamento.NTSSetParam(oMenu, oApp.Tr(Me, 128578217597827908, "Arrotondamento"), "#,##0.0000")
      edPerc1.NTSSetParam(oMenu, oApp.Tr(Me, 128578218501739943, "Percentuale Margine"), "#,##0.000000", 8, 0, 99999999)
      edPerc2.NTSSetParam(oMenu, oApp.Tr(Me, 128578218699241207, "Percentuale Ricarico"), "#,##0.000000", 8, 0, 99999999)
      edRelazioneListini.NTSSetParam(oMenu, oApp.Tr(Me, 128580991627795682, "Relazione Listini"), 0)
      edPrezzoNew1.NTSSetParam(oMenu, oApp.Tr(Me, 128578219412058269, "Nuovo Prezzo (Listino 1)"), oApp.FormatPrzUn)
      edPrezzoNew2.NTSSetParam(oMenu, oApp.Tr(Me, 128578219427839620, "Nuovo Prezzo (Listino 2)"), oApp.FormatPrzUn)
      edPrezzoNew3.NTSSetParam(oMenu, oApp.Tr(Me, 128578219472214904, "Nuovo Prezzo (Listino 3)"), oApp.FormatPrzUn)
      edPrezzoNew4.NTSSetParam(oMenu, oApp.Tr(Me, 128578219489246263, "Nuovo Prezzo (Listino 4)"), oApp.FormatPrzUn)
      edPrezzoNew5.NTSSetParam(oMenu, oApp.Tr(Me, 128578219506433873, "Nuovo Prezzo (Listino 5)"), oApp.FormatPrzUn)
      edPrezzoNew6.NTSSetParam(oMenu, oApp.Tr(Me, 128578219522527726, "Nuovo Prezzo (Listino 6)"), oApp.FormatPrzUn)
      edPrezzoNew7.NTSSetParam(oMenu, oApp.Tr(Me, 128578219541121595, "Nuovo Prezzo (Listino 7)"), oApp.FormatPrzUn)
      edPrezzoNew8.NTSSetParam(oMenu, oApp.Tr(Me, 128578219556121691, "Nuovo Prezzo (Listino 8)"), oApp.FormatPrzUn)
      edPrezzoOld1.NTSSetParam(oMenu, oApp.Tr(Me, 128578220164406834, "Vecchio Prezzo (Listino 1)"), oApp.FormatPrzUn)
      edPrezzoOld2.NTSSetParam(oMenu, oApp.Tr(Me, 128578220179563181, "Vecchio Prezzo (Listino 2)"), oApp.FormatPrzUn)
      edPrezzoOld3.NTSSetParam(oMenu, oApp.Tr(Me, 128578220194407026, "Vecchio Prezzo (Listino 3)"), oApp.FormatPrzUn)
      edPrezzoOld4.NTSSetParam(oMenu, oApp.Tr(Me, 128578220363626859, "Vecchio Prezzo (Listino 4)"), oApp.FormatPrzUn)
      edPrezzoOld5.NTSSetParam(oMenu, oApp.Tr(Me, 128578220380501967, "Vecchio Prezzo (Listino 5)"), oApp.FormatPrzUn)
      edPrezzoOld6.NTSSetParam(oMenu, oApp.Tr(Me, 128578220397689577, "Vecchio Prezzo (Listino 6)"), oApp.FormatPrzUn)
      edPrezzoOld7.NTSSetParam(oMenu, oApp.Tr(Me, 128578220413470928, "Vecchio Prezzo (Listino 7)"), oApp.FormatPrzUn)
      edPrezzoOld8.NTSSetParam(oMenu, oApp.Tr(Me, 128578220429877283, "Vecchio Prezzo (Listino 8)"), oApp.FormatPrzUn)
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "FORM"
  Public Overridable Sub FRMMGRCPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim dUltCos As Decimal = 0
    Dim dDaQuant As Decimal = 0
    Dim dAQuant As Decimal = 9999999999
    Dim strDespdon As String = ""

    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      '--- Legge i parametri in ingresso
      '----------------------------------------------------------------------------------
      oCallParams.Ditta = DittaCorrente
      oCleRcpr.lContoIn = NTSCInt(oCallParams.dPar1)
      oCleRcpr.nValutaIn = NTSCInt(oCallParams.dPar2)
      oCleRcpr.nFaseIn = NTSCInt(oCallParams.dPar3)
      oCleRcpr.dQuantIn = NTSCDec(oCallParams.dPar4)
      oCleRcpr.dPrezzoBase = NTSCDec(oCallParams.dPar5)
      oCleRcpr.strCodartIn = oCallParams.strPar1
      oCleRcpr.dtDatordIn = NTSCDate(oCallParams.strPar2)
      oCleRcpr.strUnmisIn = oCallParams.strPar3
      oCleRcpr.strFlprznetIn = oCallParams.strPar4
      '----------------------------------------------------------------------------------
      '--- Setta i controlli e le varibili necessarie per l'elaborazione
      '----------------------------------------------------------------------------------
      oCleRcpr.InizializzaCampi(DittaCorrente, oCleRcpr.strCodartIn, True, _
        oCleRcpr.dttArtico, strDespdon, dUltCos, dDaQuant, dAQuant, _
        dPrezzoNew, dPrezzoOld, dtDataOld)
      '----------------------------------------------------------------------------------
      edCodart.Text = oCleRcpr.dttArtico.Rows(0)!ar_codart.ToString
      lbDescodart.Text = oCleRcpr.dttArtico.Rows(0)!ar_descr.ToString
      edPrezzoBase.Text = oCleRcpr.dPrezzoBase.ToString
      edUltimoCosto.Text = dUltCos.ToString
      '----------------------------------------------------------------------------------
      '--- Controlli
      '----------------------------------------------------------------------------------
      edDataEffetto.Text = oCleRcpr.dtDatordIn.ToString
      edQuantIni.Text = dDaQuant.ToString
      edQuantFin.Text = dAQuant.ToString
      edRicMar.Text = IIf(oCleRcpr.dttArtico.Rows(0)!ar_flricmar.ToString = "R", "Ricarico", "Margine").ToString
      edArrotondamento.Text = IntSetNum("0,01")
      edPerc1.Text = oCleRcpr.dttArtico.Rows(0)!ar_ricar1.ToString
      edPerc2.Text = oCleRcpr.dttArtico.Rows(0)!ar_ricar2.ToString
      edRelazioneListini.Text = strDespdon
      '----------------------------------------------------------------------------------
      oCleRcpr.Esegui(DittaCorrente, oCleRcpr.strCodartIn, oCleRcpr.dttArtico, _
        NTSCDec(edPerc1.Text), NTSCDec(edPerc2.Text), NTSCDec(edArrotondamento.Text), _
        dPrezzoNew, dPrezzoOld, dtDataOld)
      '----------------------------------------------------------------------------------
      edPrezzoNew1.Text = dPrezzoNew(1).ToString
      edPrezzoNew2.Text = dPrezzoNew(2).ToString
      edPrezzoNew3.Text = dPrezzoNew(3).ToString
      edPrezzoNew4.Text = dPrezzoNew(4).ToString
      edPrezzoNew5.Text = dPrezzoNew(5).ToString
      edPrezzoNew6.Text = dPrezzoNew(6).ToString
      edPrezzoNew7.Text = dPrezzoNew(7).ToString
      edPrezzoNew8.Text = dPrezzoNew(8).ToString
      edPrezzoOld1.Text = dPrezzoOld(1).ToString
      edPrezzoOld2.Text = dPrezzoOld(2).ToString
      edPrezzoOld3.Text = dPrezzoOld(3).ToString
      edPrezzoOld4.Text = dPrezzoOld(4).ToString
      edPrezzoOld5.Text = dPrezzoOld(5).ToString
      edPrezzoOld6.Text = dPrezzoOld(6).ToString
      edPrezzoOld7.Text = dPrezzoOld(7).ToString
      edPrezzoOld8.Text = dPrezzoOld(8).ToString
      edDataPrec1.Text = dtDataOld(1).ToString
      edDataPrec2.Text = dtDataOld(2).ToString
      edDataPrec3.Text = dtDataOld(3).ToString
      edDataPrec4.Text = dtDataOld(4).ToString
      edDataPrec5.Text = dtDataOld(5).ToString
      edDataPrec6.Text = dtDataOld(6).ToString
      edDataPrec7.Text = dtDataOld(7).ToString
      edDataPrec8.Text = dtDataOld(8).ToString
      '----------------------------------------------------------------------------------
      If dPrezzoNew(1) = 0 Then
        edPrezzoNew1.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew1, False)
      End If
      If dPrezzoNew(2) = 0 Then
        edPrezzoNew2.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew2, False)
      End If
      If dPrezzoNew(3) = 0 Then
        edPrezzoNew3.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew3, False)
      End If
      If dPrezzoNew(4) = 0 Then
        edPrezzoNew4.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew1, False)
      End If
      If dPrezzoNew(5) = 0 Then
        edPrezzoNew5.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew5, False)
      End If
      If dPrezzoNew(6) = 0 Then
        edPrezzoNew6.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew1, False)
      End If
      If dPrezzoNew(7) = 0 Then
        edPrezzoNew7.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew1, False)
      End If
      If dPrezzoNew(8) = 0 Then
        edPrezzoNew8.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew1, False)
      End If
      '----------------------------------------------------------------------------------
      '--- Calcola la percentuale di scostamento
      '----------------------------------------------------------------------------------
      CalcolaScostamento()
      '----------------------------------------------------------------------------------
      '--- Variabili
      '----------------------------------------------------------------------------------
      oCleRcpr.dPerqta = NTSCDec(oCleRcpr.dttArtico.Rows(0)!ar_perqta.ToString)
      oCleRcpr.strUnmis = oCleRcpr.dttArtico.Rows(0)!ar_unmis.ToString
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "TEXTBOX"
  Public Overridable Sub edPrezzoBase_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edPrezzoBase.Validated
    Try
      CalcolaScostamento()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "PULSANTI"
  Public Overridable Sub cmdRicalcola_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicalcola.Click
    Dim strMsg As String = ""

    Try
      '----------------------------------------------------------------------------------
      If NTSCDec(edArrotondamento.Text) = 0 Then
        edArrotondamento.Text = IntSetNum("0,01")
        strMsg = "Attenzione!" & vbCrLf & _
          "Il valore indicato nel campo '|" & lbArrotondamento.Text & "|' deve essere diverso da zero."
        oApp.MsgBoxErr(oApp.Tr(Me, 129496046529560547, strMsg))
        Return
      End If
      '----------------------------------------------------------------------------------
      If edRicMar.Text = "Margine" Then
        If (NTSCDec(edPerc1.Text) + NTSCDec(edPerc2.Text)) = 100 Then
          strMsg = "Attenzione!" & vbCrLf & _
            "La somma delle 2 percentuali di 'Margine' non può essere uguale a 100."
          oApp.MsgBoxErr(oApp.Tr(Me, 128581041205998071, strMsg))
          Return
        End If
      End If
      '----------------------------------------------------------------------------------
      oCleRcpr.dPrezzoBase = NTSCDec(edPrezzoBase.Text)
      oCleRcpr.Esegui(DittaCorrente, oCleRcpr.strCodartIn, oCleRcpr.dttArtico, _
        NTSCDec(edPerc1.Text), NTSCDec(edPerc2.Text), NTSCDec(edArrotondamento.Text), _
        dPrezzoNew, dPrezzoOld, dtDataOld)
      '----------------------------------------------------------------------------------
      edPrezzoNew1.Text = dPrezzoNew(1).ToString
      edPrezzoNew2.Text = dPrezzoNew(2).ToString
      edPrezzoNew3.Text = dPrezzoNew(3).ToString
      edPrezzoNew4.Text = dPrezzoNew(4).ToString
      edPrezzoNew5.Text = dPrezzoNew(5).ToString
      edPrezzoNew6.Text = dPrezzoNew(6).ToString
      edPrezzoNew7.Text = dPrezzoNew(7).ToString
      edPrezzoNew8.Text = dPrezzoNew(8).ToString
      edPrezzoOld1.Text = dPrezzoOld(1).ToString
      edPrezzoOld2.Text = dPrezzoOld(2).ToString
      edPrezzoOld3.Text = dPrezzoOld(3).ToString
      edPrezzoOld4.Text = dPrezzoOld(4).ToString
      edPrezzoOld5.Text = dPrezzoOld(5).ToString
      edPrezzoOld6.Text = dPrezzoOld(6).ToString
      edPrezzoOld7.Text = dPrezzoOld(7).ToString
      edPrezzoOld8.Text = dPrezzoOld(8).ToString
      edDataPrec1.Text = dtDataOld(1).ToString
      edDataPrec2.Text = dtDataOld(2).ToString
      edDataPrec3.Text = dtDataOld(3).ToString
      edDataPrec4.Text = dtDataOld(4).ToString
      edDataPrec5.Text = dtDataOld(5).ToString
      edDataPrec6.Text = dtDataOld(6).ToString
      edDataPrec7.Text = dtDataOld(7).ToString
      edDataPrec8.Text = dtDataOld(8).ToString
      '----------------------------------------------------------------------------------
      If dPrezzoNew(1) = 0 Then
        edPrezzoNew1.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew1, False)
      End If
      If dPrezzoNew(2) = 0 Then
        edPrezzoNew2.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew2, False)
      End If
      If dPrezzoNew(3) = 0 Then
        edPrezzoNew3.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew3, False)
      End If
      If dPrezzoNew(4) = 0 Then
        edPrezzoNew4.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew4, False)
      End If
      If dPrezzoNew(5) = 0 Then
        edPrezzoNew5.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew5, False)
      End If
      If dPrezzoNew(6) = 0 Then
        edPrezzoNew6.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew6, False)
      End If
      If dPrezzoNew(7) = 0 Then
        edPrezzoNew7.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew7, False)
      End If
      If dPrezzoNew(8) = 0 Then
        edPrezzoNew8.Enabled = False
      Else
        GctlSetVisEnab(edPrezzoNew8, False)
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Dim i As Integer = 0

    Try
      '----------------------------------------------------------------------------------
      CaricaArrayDaTxt() 'per leggere/aggiornare il valore dei listini digitati sulle textbox
      '----------------------------------------------------------------------------------
      oCleRcpr.AggiornaPrezzi(DittaCorrente, oCleRcpr.dttArtico, _
        NTSCDec(edQuantIni.Text), NTSCDec(edQuantFin.Text), NTSCDate(edDataEffetto.Text), _
        dPrezzoNewTxt)
      '---------------------------------------------------------------------------------
      oCleRcpr.Esegui(DittaCorrente, oCleRcpr.strCodartIn, oCleRcpr.dttArtico, _
        NTSCDec(edPerc1.Text), NTSCDec(edPerc2.Text), NTSCDec(edArrotondamento.Text), _
        dPrezzoNew, dPrezzoOld, dtDataOld)
      edPrezzoNew1.Text = dPrezzoNew(1).ToString
      edPrezzoNew2.Text = dPrezzoNew(2).ToString
      edPrezzoNew3.Text = dPrezzoNew(3).ToString
      edPrezzoNew4.Text = dPrezzoNew(4).ToString
      edPrezzoNew5.Text = dPrezzoNew(5).ToString
      edPrezzoNew6.Text = dPrezzoNew(6).ToString
      edPrezzoNew7.Text = dPrezzoNew(7).ToString
      edPrezzoNew8.Text = dPrezzoNew(8).ToString
      edPrezzoOld1.Text = dPrezzoOld(1).ToString
      edPrezzoOld2.Text = dPrezzoOld(2).ToString
      edPrezzoOld3.Text = dPrezzoOld(3).ToString
      edPrezzoOld4.Text = dPrezzoOld(4).ToString
      edPrezzoOld5.Text = dPrezzoOld(5).ToString
      edPrezzoOld6.Text = dPrezzoOld(6).ToString
      edPrezzoOld7.Text = dPrezzoOld(7).ToString
      edPrezzoOld8.Text = dPrezzoOld(8).ToString
      edDataPrec1.Text = dtDataOld(1).ToString
      edDataPrec2.Text = dtDataOld(2).ToString
      edDataPrec3.Text = dtDataOld(3).ToString
      edDataPrec4.Text = dtDataOld(4).ToString
      edDataPrec5.Text = dtDataOld(5).ToString
      edDataPrec6.Text = dtDataOld(6).ToString
      edDataPrec7.Text = dtDataOld(7).ToString
      edDataPrec8.Text = dtDataOld(8).ToString
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub
#End Region

#Region "ROUTINE"
  Public Overridable Sub CalcolaScostamento()
    Dim dScostamento As Decimal = 0
    Try
      '----------------------------------------------------------------------------------
      dScostamento = 0
      '----------------------------------------------------------------------------------
      If NTSCDec(edPrezzoBase.Text) <> 0 Then
        dScostamento = ArrDbl((((NTSCDec(edPrezzoBase.Text) - NTSCDec(edUltimoCosto.Text)) / NTSCDec(edPrezzoBase.Text)) * 100), 2)
      End If
      '----------------------------------------------------------------------------------------
      edScostamento.Text = dScostamento.ToString
      '----------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Sub
  Public Overridable Sub CaricaArrayDaTxt()
    Try
      dPrezzoNewTxt(1) = NTSCDec(edPrezzoNew1.Text)
      dPrezzoNewTxt(2) = NTSCDec(edPrezzoNew2.Text)
      dPrezzoNewTxt(3) = NTSCDec(edPrezzoNew3.Text)
      dPrezzoNewTxt(4) = NTSCDec(edPrezzoNew4.Text)
      dPrezzoNewTxt(5) = NTSCDec(edPrezzoNew5.Text)
      dPrezzoNewTxt(6) = NTSCDec(edPrezzoNew6.Text)
      dPrezzoNewTxt(7) = NTSCDec(edPrezzoNew7.Text)
      dPrezzoNewTxt(8) = NTSCDec(edPrezzoNew8.Text)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Sub
#End Region

End Class
