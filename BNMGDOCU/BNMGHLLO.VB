Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGHLLO

#Region "Variabili"
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsHllo As New DataSet
  Public dcHllo As New BindingSource()
  Public strParent As String = ""     'child che mi ha chiamato
  Public dQtaTot As Decimal = 0       'qta totale da evadere, passata dal chiamate
  Public dttRkOut As New DataTable    'per multiselezione, righe selezioante con qta <> 0
  Public components As System.ComponentModel.IContainer

  Public WithEvents xx_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codalf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispnetta As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dtprep As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dtscad As NTSInformatica.NTSGridColumn
  Public WithEvents xx_note As NTSInformatica.NTSGridColumn
  Public WithEvents xx_disponibilita As NTSInformatica.NTSGridColumn
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents grHllo As NTSInformatica.NTSGrid
  Public WithEvents grvHllo As NTSInformatica.NTSGridView
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents lbXx_Fase As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Magaz As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codart As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents xx_qtaeva As NTSInformatica.NTSGridColumn
  Public WithEvents cmdAssegnaLotti As NTSInformatica.NTSButton
  Public WithEvents xx_esist As NTSInformatica.NTSGridColumn
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
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGDOCU", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086828294429425, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGDOCU", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGHLLO))
    Me.xx_ubicaz = New NTSInformatica.NTSGridColumn
    Me.xx_lottox = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.xx_codalf = New NTSInformatica.NTSGridColumn
    Me.xx_esist = New NTSInformatica.NTSGridColumn
    Me.xx_dispnetta = New NTSInformatica.NTSGridColumn
    Me.xx_dtprep = New NTSInformatica.NTSGridColumn
    Me.xx_dtscad = New NTSInformatica.NTSGridColumn
    Me.xx_note = New NTSInformatica.NTSGridColumn
    Me.xx_disponibilita = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdAssegnaLotti = New NTSInformatica.NTSButton
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_Fase = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Magaz = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_Codart = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.grHllo = New NTSInformatica.NTSGrid
    Me.grvHllo = New NTSInformatica.NTSGridView
    Me.xx_qtaeva = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grHllo, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHllo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
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
    'xx_ubicaz
    '
    Me.xx_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ubicaz.Caption = "Ubicazione"
    Me.xx_ubicaz.Enabled = False
    Me.xx_ubicaz.FieldName = "xx_ubicaz"
    Me.xx_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ubicaz.Name = "xx_ubicaz"
    Me.xx_ubicaz.NTSRepositoryComboBox = Nothing
    Me.xx_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.xx_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.xx_ubicaz.NTSRepositoryItemText = Nothing
    Me.xx_ubicaz.OptionsColumn.AllowEdit = False
    Me.xx_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ubicaz.OptionsColumn.ReadOnly = True
    Me.xx_ubicaz.OptionsFilter.AllowFilter = False
    Me.xx_ubicaz.Visible = True
    Me.xx_ubicaz.VisibleIndex = 1
    '
    'xx_lottox
    '
    Me.xx_lottox.AppearanceCell.Options.UseBackColor = True
    Me.xx_lottox.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lottox.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lottox.Caption = "Lotto"
    Me.xx_lottox.Enabled = False
    Me.xx_lottox.FieldName = "xx_lottox"
    Me.xx_lottox.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lottox.Name = "xx_lottox"
    Me.xx_lottox.NTSRepositoryComboBox = Nothing
    Me.xx_lottox.NTSRepositoryItemCheck = Nothing
    Me.xx_lottox.NTSRepositoryItemMemo = Nothing
    Me.xx_lottox.NTSRepositoryItemText = Nothing
    Me.xx_lottox.OptionsColumn.AllowEdit = False
    Me.xx_lottox.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lottox.OptionsColumn.ReadOnly = True
    Me.xx_lottox.OptionsFilter.AllowFilter = False
    Me.xx_lottox.Visible = True
    Me.xx_lottox.VisibleIndex = 3
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descr."
    Me.xx_descr.Enabled = False
    Me.xx_descr.FieldName = "xx_descr"
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowEdit = False
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsColumn.ReadOnly = True
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 4
    '
    'xx_codalf
    '
    Me.xx_codalf.AppearanceCell.Options.UseBackColor = True
    Me.xx_codalf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codalf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codalf.Caption = "Ident."
    Me.xx_codalf.Enabled = False
    Me.xx_codalf.FieldName = "xx_codalf"
    Me.xx_codalf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codalf.Name = "xx_codalf"
    Me.xx_codalf.NTSRepositoryComboBox = Nothing
    Me.xx_codalf.NTSRepositoryItemCheck = Nothing
    Me.xx_codalf.NTSRepositoryItemMemo = Nothing
    Me.xx_codalf.NTSRepositoryItemText = Nothing
    Me.xx_codalf.OptionsColumn.AllowEdit = False
    Me.xx_codalf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codalf.OptionsColumn.ReadOnly = True
    Me.xx_codalf.OptionsFilter.AllowFilter = False
    Me.xx_codalf.Visible = True
    Me.xx_codalf.VisibleIndex = 5
    '
    'xx_esist
    '
    Me.xx_esist.AppearanceCell.Options.UseBackColor = True
    Me.xx_esist.AppearanceCell.Options.UseTextOptions = True
    Me.xx_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_esist.Caption = "Esistenza"
    Me.xx_esist.Enabled = False
    Me.xx_esist.FieldName = "xx_esist"
    Me.xx_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_esist.Name = "xx_esist"
    Me.xx_esist.NTSRepositoryComboBox = Nothing
    Me.xx_esist.NTSRepositoryItemCheck = Nothing
    Me.xx_esist.NTSRepositoryItemMemo = Nothing
    Me.xx_esist.NTSRepositoryItemText = Nothing
    Me.xx_esist.OptionsColumn.AllowEdit = False
    Me.xx_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_esist.OptionsColumn.ReadOnly = True
    Me.xx_esist.OptionsFilter.AllowFilter = False
    Me.xx_esist.Visible = True
    Me.xx_esist.VisibleIndex = 6
    '
    'xx_dispnetta
    '
    Me.xx_dispnetta.AppearanceCell.Options.UseBackColor = True
    Me.xx_dispnetta.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dispnetta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dispnetta.Caption = "Dispon. netta"
    Me.xx_dispnetta.Enabled = False
    Me.xx_dispnetta.FieldName = "xx_dispnetta"
    Me.xx_dispnetta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dispnetta.Name = "xx_dispnetta"
    Me.xx_dispnetta.NTSRepositoryComboBox = Nothing
    Me.xx_dispnetta.NTSRepositoryItemCheck = Nothing
    Me.xx_dispnetta.NTSRepositoryItemMemo = Nothing
    Me.xx_dispnetta.NTSRepositoryItemText = Nothing
    Me.xx_dispnetta.OptionsColumn.AllowEdit = False
    Me.xx_dispnetta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dispnetta.OptionsColumn.ReadOnly = True
    Me.xx_dispnetta.OptionsFilter.AllowFilter = False
    Me.xx_dispnetta.Visible = True
    Me.xx_dispnetta.VisibleIndex = 7
    '
    'xx_dtprep
    '
    Me.xx_dtprep.AppearanceCell.Options.UseBackColor = True
    Me.xx_dtprep.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dtprep.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dtprep.Caption = "Data prep."
    Me.xx_dtprep.Enabled = False
    Me.xx_dtprep.FieldName = "xx_dtprep"
    Me.xx_dtprep.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dtprep.Name = "xx_dtprep"
    Me.xx_dtprep.NTSRepositoryComboBox = Nothing
    Me.xx_dtprep.NTSRepositoryItemCheck = Nothing
    Me.xx_dtprep.NTSRepositoryItemMemo = Nothing
    Me.xx_dtprep.NTSRepositoryItemText = Nothing
    Me.xx_dtprep.OptionsColumn.AllowEdit = False
    Me.xx_dtprep.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dtprep.OptionsColumn.ReadOnly = True
    Me.xx_dtprep.OptionsFilter.AllowFilter = False
    Me.xx_dtprep.Visible = True
    Me.xx_dtprep.VisibleIndex = 8
    '
    'xx_dtscad
    '
    Me.xx_dtscad.AppearanceCell.Options.UseBackColor = True
    Me.xx_dtscad.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dtscad.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dtscad.Caption = "Data scad."
    Me.xx_dtscad.Enabled = False
    Me.xx_dtscad.FieldName = "xx_dtscad"
    Me.xx_dtscad.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dtscad.Name = "xx_dtscad"
    Me.xx_dtscad.NTSRepositoryComboBox = Nothing
    Me.xx_dtscad.NTSRepositoryItemCheck = Nothing
    Me.xx_dtscad.NTSRepositoryItemMemo = Nothing
    Me.xx_dtscad.NTSRepositoryItemText = Nothing
    Me.xx_dtscad.OptionsColumn.AllowEdit = False
    Me.xx_dtscad.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dtscad.OptionsColumn.ReadOnly = True
    Me.xx_dtscad.OptionsFilter.AllowFilter = False
    Me.xx_dtscad.Visible = True
    Me.xx_dtscad.VisibleIndex = 9
    '
    'xx_note
    '
    Me.xx_note.AppearanceCell.Options.UseBackColor = True
    Me.xx_note.AppearanceCell.Options.UseTextOptions = True
    Me.xx_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_note.Caption = "Note"
    Me.xx_note.Enabled = True
    Me.xx_note.FieldName = "xx_note"
    Me.xx_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_note.Name = "xx_note"
    Me.xx_note.NTSRepositoryComboBox = Nothing
    Me.xx_note.NTSRepositoryItemCheck = Nothing
    Me.xx_note.NTSRepositoryItemMemo = Nothing
    Me.xx_note.NTSRepositoryItemText = Nothing
    Me.xx_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_note.OptionsFilter.AllowFilter = False
    '
    'xx_disponibilita
    '
    Me.xx_disponibilita.AppearanceCell.Options.UseBackColor = True
    Me.xx_disponibilita.AppearanceCell.Options.UseTextOptions = True
    Me.xx_disponibilita.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_disponibilita.Caption = "Disponibilit‡"
    Me.xx_disponibilita.Enabled = False
    Me.xx_disponibilita.FieldName = "xx_disponibilita"
    Me.xx_disponibilita.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_disponibilita.Name = "xx_disponibilita"
    Me.xx_disponibilita.NTSRepositoryComboBox = Nothing
    Me.xx_disponibilita.NTSRepositoryItemCheck = Nothing
    Me.xx_disponibilita.NTSRepositoryItemMemo = Nothing
    Me.xx_disponibilita.NTSRepositoryItemText = Nothing
    Me.xx_disponibilita.OptionsColumn.AllowEdit = False
    Me.xx_disponibilita.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_disponibilita.OptionsColumn.ReadOnly = True
    Me.xx_disponibilita.OptionsFilter.AllowFilter = False
    Me.xx_disponibilita.Visible = True
    Me.xx_disponibilita.VisibleIndex = 2
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdAssegnaLotti)
    Me.pnTop.Controls.Add(Me.edCodart)
    Me.pnTop.Controls.Add(Me.lbXx_Fase)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.lbXx_Magaz)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.edMagaz)
    Me.pnTop.Controls.Add(Me.lbXx_Codart)
    Me.pnTop.Controls.Add(Me.lbCodart)
    Me.pnTop.Controls.Add(Me.cmdAnnulla)
    Me.pnTop.Controls.Add(Me.cmdConferma)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(667, 98)
    Me.pnTop.TabIndex = 4
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdAssegnaLotti
    '
    Me.cmdAssegnaLotti.ImagePath = ""
    Me.cmdAssegnaLotti.ImageText = ""
    Me.cmdAssegnaLotti.Location = New System.Drawing.Point(542, 69)
    Me.cmdAssegnaLotti.Name = "cmdAssegnaLotti"
    Me.cmdAssegnaLotti.NTSContextMenu = Nothing
    Me.cmdAssegnaLotti.Size = New System.Drawing.Size(113, 26)
    Me.cmdAssegnaLotti.TabIndex = 677
    Me.cmdAssegnaLotti.Text = "Assegna lotti autom."
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(106, 10)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.AutoHeight = False
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(147, 20)
    Me.edCodart.TabIndex = 676
    '
    'lbXx_Fase
    '
    Me.lbXx_Fase.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Fase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Fase.Location = New System.Drawing.Point(178, 62)
    Me.lbXx_Fase.Name = "lbXx_Fase"
    Me.lbXx_Fase.NTSDbField = ""
    Me.lbXx_Fase.Size = New System.Drawing.Size(344, 20)
    Me.lbXx_Fase.TabIndex = 675
    Me.lbXx_Fase.Tooltip = ""
    Me.lbXx_Fase.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(17, 65)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(30, 13)
    Me.lbFase.TabIndex = 673
    Me.lbFase.Text = "Fase"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.EditValue = "0"
    Me.edFase.Location = New System.Drawing.Point(106, 62)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.AutoHeight = False
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(66, 20)
    Me.edFase.TabIndex = 674
    '
    'lbXx_Magaz
    '
    Me.lbXx_Magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Magaz.Location = New System.Drawing.Point(178, 36)
    Me.lbXx_Magaz.Name = "lbXx_Magaz"
    Me.lbXx_Magaz.NTSDbField = ""
    Me.lbXx_Magaz.Size = New System.Drawing.Size(344, 20)
    Me.lbXx_Magaz.TabIndex = 672
    Me.lbXx_Magaz.Tooltip = ""
    Me.lbXx_Magaz.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(17, 39)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(57, 13)
    Me.lbMagaz.TabIndex = 670
    Me.lbMagaz.Text = "Magazzino"
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.EditValue = "0"
    Me.edMagaz.Location = New System.Drawing.Point(106, 36)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.AutoHeight = False
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(66, 20)
    Me.edMagaz.TabIndex = 671
    '
    'lbXx_Codart
    '
    Me.lbXx_Codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codart.Location = New System.Drawing.Point(259, 12)
    Me.lbXx_Codart.Name = "lbXx_Codart"
    Me.lbXx_Codart.NTSDbField = ""
    Me.lbXx_Codart.Size = New System.Drawing.Size(263, 20)
    Me.lbXx_Codart.TabIndex = 669
    Me.lbXx_Codart.Tooltip = ""
    Me.lbXx_Codart.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(17, 15)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(43, 13)
    Me.lbCodart.TabIndex = 667
    Me.lbCodart.Text = "Articolo"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(542, 38)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(113, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(542, 12)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(113, 26)
    Me.cmdConferma.TabIndex = 0
    Me.cmdConferma.Text = "Conferma"
    '
    'grHllo
    '
    Me.grHllo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grHllo.EmbeddedNavigator.Name = ""
    Me.grHllo.Location = New System.Drawing.Point(0, 98)
    Me.grHllo.MainView = Me.grvHllo
    Me.grHllo.Name = "grHllo"
    Me.grHllo.Size = New System.Drawing.Size(667, 268)
    Me.grHllo.TabIndex = 5
    Me.grHllo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHllo})
    '
    'grvHllo
    '
    Me.grvHllo.ActiveFilterEnabled = False
    Me.grvHllo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_qtaeva, Me.xx_ubicaz, Me.xx_disponibilita, Me.xx_lottox, Me.xx_descr, Me.xx_codalf, Me.xx_esist, Me.xx_dispnetta, Me.xx_dtprep, Me.xx_dtscad, Me.xx_note})
    Me.grvHllo.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHllo.Enabled = True
    Me.grvHllo.GridControl = Me.grHllo
    Me.grvHllo.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHllo.MinRowHeight = 14
    Me.grvHllo.Name = "grvHllo"
    Me.grvHllo.NTSAllowDelete = True
    Me.grvHllo.NTSAllowInsert = True
    Me.grvHllo.NTSAllowUpdate = True
    Me.grvHllo.NTSMenuContext = Nothing
    Me.grvHllo.OptionsCustomization.AllowRowSizing = True
    Me.grvHllo.OptionsFilter.AllowFilterEditor = False
    Me.grvHllo.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHllo.OptionsNavigation.UseTabKey = False
    Me.grvHllo.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHllo.OptionsView.ColumnAutoWidth = False
    Me.grvHllo.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHllo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHllo.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHllo.OptionsView.ShowGroupPanel = False
    Me.grvHllo.RowHeight = 16
    '
    'xx_qtaeva
    '
    Me.xx_qtaeva.AppearanceCell.Options.UseBackColor = True
    Me.xx_qtaeva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_qtaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_qtaeva.Caption = "Qta da assegnare"
    Me.xx_qtaeva.Enabled = True
    Me.xx_qtaeva.FieldName = "xx_qtaeva"
    Me.xx_qtaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_qtaeva.Name = "xx_qtaeva"
    Me.xx_qtaeva.NTSRepositoryComboBox = Nothing
    Me.xx_qtaeva.NTSRepositoryItemCheck = Nothing
    Me.xx_qtaeva.NTSRepositoryItemMemo = Nothing
    Me.xx_qtaeva.NTSRepositoryItemText = Nothing
    Me.xx_qtaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_qtaeva.OptionsFilter.AllowFilter = False
    Me.xx_qtaeva.Visible = True
    Me.xx_qtaeva.VisibleIndex = 0
    '
    'FRMMGHLLO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(667, 366)
    Me.Controls.Add(Me.grHllo)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.HelpContext = ""
    Me.Name = "FRMMGHLLO"
    Me.Text = "SALDO LOTTI/UBICAZIONI APERTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grHllo, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHllo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvHllo.NTSSetParam(oMenu, oApp.Tr(Me, 130086828516148175, "Lotti ubicazioni"))
      xx_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227376545229, "Ubicazione"), 0, True)
      xx_lottox.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227376701482, "Lotto"), 50, True)
      xx_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227376857735, "Descr."), 0, True)
      xx_codalf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227377013988, "Ident."), 0, True)
      xx_esist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883227377170241, "Esistenza"), oApp.FormatQta, 15)
      xx_dispnetta.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883227377326494, "Dispon. netta"), oApp.FormatQta, 15)
      xx_dtprep.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128883227377482747, "Data prep."), True)
      xx_dtscad.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128883227377639000, "Data scad."), True)
      xx_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883227377795253, "Note"), 0, True)
      xx_disponibilita.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883227377951506, "Disponibilit‡"), oApp.FormatQta, 15)
      xx_qtaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 130699320496623205, "Qta da assegnare"), oApp.FormatQta, 15, 0, dQtaTot)

      edCodart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128883227380451554, "Cod. articolo"), tabartico, True)
      edFase.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128883227380920313, "Cod. articolo"), tabartfasi)
      edFase.ArtiPerFase = edCodart
      edMagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128883227381389072, "Cod. articolo"), tabmaga)

      edCodart.Enabled = False
      edMagaz.Enabled = False
      edFase.Enabled = False

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
  Public Overridable Sub InitEntity(ByRef cleDocu As CLEMGDOCU)
    oCleDocu = cleDocu
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub Apri()
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleDocu.HlloApri(DittaCorrente, dsHllo) Then
        Me.Close()
        Return
      End If

      dcHllo.DataSource = Nothing
      dcHllo.DataSource = dsHllo.Tables("LOTCPRO")
      grHllo.DataSource = dcHllo
      dsHllo.AcceptChanges()

      grvHllo.NTSAllowInsert = False
      grvHllo.NTSAllowUpdate = False
      grvHllo.NTSAllowDelete = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRMMGHLLO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttTmp As New DataTable
    Try
      If dQtaTot <= 0 Then dQtaTot = 999999999
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '----------------------------------------------------------------------------------------
      '--- nTipoGestArticolo:
      '--- 1 --> Articolo gestito solo a Lotti
      '--- 2 --> Articolo gestito solo a Ubicazioni
      '--- 3 --> Articolo gestito sia a Lotti che a Ubicazioni
      '----------------------------------------------------------------------------------------
      If oCleDocu.Artico_Validated(oCleDocu.strHlloCodart, dttTmp) Then
        With dttTmp.Rows(0)
          If NTSCStr(!ar_geslotti) = "S" And NTSCStr(!ar_gesubic) = "N" Then
            oCleDocu.nTipoGestArticolo = 1
          Else
            cmdAssegnaLotti.Visible = False 'potrei assegnare i lotti, ma non ho una routine che mi dice con quale criterio assegnare per ubicazione
            If NTSCStr(!ar_geslotti) = "N" And NTSCStr(!ar_gesubic) = "S" Then
              oCleDocu.nTipoGestArticolo = 2
            Else
              oCleDocu.nTipoGestArticolo = 3
            End If
          End If
        End With
      End If

      Select Case oCleDocu.nTipoGestArticolo
        Case 1   '--- Articolo gestito solo a Lotti
          '------------------------------------------------------------------------------------
          '--- Nasconde le colonne relative all'ubicazione
          '------------------------------------------------------------------------------------
          xx_ubicaz.Visible = False
          '------------------------------------------------------------------------------------
          Me.Text = oApp.Tr(Me, 130086830008023175, "Saldo Lotti aperti")
          '------------------------------------------------------------------------------------
        Case 2   '--- Articolo gestito solo a Ubicazioni
          '------------------------------------------------------------------------------------
          '--- Nasconde le colonne relative al Lotto
          '------------------------------------------------------------------------------------
          xx_lottox.Visible = False
          xx_descr.Visible = False
          xx_codalf.Visible = False
          xx_dtprep.Visible = False
          xx_dtscad.Visible = False
          xx_note.Visible = False
          '------------------------------------------------------------------------------------
          Me.Text = oApp.Tr(Me, 130086830100523175, "Saldo Ubicazioni aperte")
          '------------------------------------------------------------------------------------
        Case 3   '--- Articolo gestito sia a Lotti che a Ubicazioni
          '------------------------------------------------------------------------------------
          Me.Text = oApp.Tr(Me, 130086830555054425, "Saldo Lotti/Ubicazioni aperti")
          '------------------------------------------------------------------------------------
      End Select

      Apri()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGHLLO_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Dim strDescr As String = ""
    Try
      If dsHllo.Tables("LOTCPRO").Rows.Count = 0 Then
        Me.Close()
      End If
      '----------------------------------------------------------------------------------------
      If dsHllo.Tables("LOTCPRO").Rows.Count = 0 Then
        cmdConferma.Enabled = False
      Else
        edCodart.Text = oCleDocu.strHlloCodart
        lbXx_Codart.Text = oCleDocu.strHlloDesart
        edMagaz.Text = NTSCStr(oCleDocu.nHlloMagaz)
        lbXx_Magaz.Text = oCleDocu.strHlloDesmaga
        edFase.Text = NTSCStr(oCleDocu.nHlloFase)
        If oCleDocu.nHlloFase <> 0 Then
          oCleDocu.Artfasi_Validated(oCleDocu.nHlloFase, oCleDocu.strHlloCodart, strDescr)
          lbXx_Fase.Text = strDescr
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGHLLO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHllo.Dispose()
      dsHllo.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dQtaTotTmp As Decimal = 0
    Try
      Me.ValidaLastControl()
      If grvHllo.NTSGetCurrentDataRow() Is Nothing Then Return
      'multiselezione
      dttRkOut.Clear()
      dsHllo.Tables("LOTCPRO").AcceptChanges()
      dttRkOut = dsHllo.Tables("LOTCPRO").Clone
      For Each dtrT As DataRow In dsHllo.Tables("LOTCPRO").Select("xx_qtaeva > 0")
        dQtaTotTmp += NTSCDec(dtrT!xx_qtaeva)
        dttRkOut.ImportRow(dtrT)
      Next
      dttRkOut.AcceptChanges()

      If dQtaTotTmp <> dQtaTot And dQtaTotTmp <> 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130699340021762224, "Attenzione: se si indica la quantit‡ da assegnare, quest'ultima (|" & dQtaTotTmp.ToString(oApp.FormatQta) & _
                                                       "|) deve essere uguale alla quantit‡ consumata (|" & dQtaTot.ToString(oApp.FormatQta) & "|)."))
        Return
      End If

      '----------------------------------------------------------------------------------------
      If dttRkOut.Rows.Count = 0 Then
        oCleDocu.lHlloLotto = NTSCInt(grvHllo.NTSGetCurrentDataRow()!xx_lotto)
        oCleDocu.strHlloUbicaz = NTSCStr(grvHllo.NTSGetCurrentDataRow()!xx_ubicaz)
        oCleDocu.dHlloQuant = ArrDbl(NTSCDec(grvHllo.NTSGetCurrentDataRow()!xx_dispnetta), 3)
        oCleDocu.nHlloTipoGestArt = oCleDocu.nTipoGestArticolo

        dttRkOut = dsHllo.Tables("LOTCPRO").Clone
        dttRkOut.ImportRow(grvHllo.NTSGetCurrentDataRow())
        dttRkOut.AcceptChanges()
      Else
        oCleDocu.lHlloLotto = NTSCInt(dttRkOut.Rows(0)!xx_lotto)
        oCleDocu.strHlloUbicaz = NTSCStr(dttRkOut.Rows(0)!xx_ubicaz)
        oCleDocu.dHlloQuant = ArrDbl(NTSCDec(dttRkOut.Rows(0)!xx_dispnetta), 3)
        oCleDocu.nHlloTipoGestArt = oCleDocu.nTipoGestArticolo
      End If
      '----------------------------------------------------------------------------------------
      oCleDocu.bHlloAnnullato = False
      '----------------------------------------------------------------------------------------
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleDocu.lHlloLotto = 0
      oCleDocu.strHlloUbicaz = ""
      oCleDocu.dHlloQuant = 0
      oCleDocu.bHlloAnnullato = True
      '----------------------------------------------------------------------------------------
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAssegnaLotti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAssegnaLotti.Click
    Dim dttLotti As New DataTable
    Dim dTmp As Decimal = 0
    Try
      If dsHllo.Tables("LOTCPRO").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130699372413786143, "Non sono presenti lotti da proporre"))
        Return
      End If
      If dsHllo.Tables("LOTCPRO").Select("xx_qtaeva > 0").Length > 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130699382834180155, "Alcune quantit‡ sono gi‡ state ssegnate. Operazione interrotta."))
        Return
      End If

      Me.Cursor = Cursors.WaitCursor

      'assegno in automatico il lotti
      If Not oCleDocu.Hllo_ProponiLottox(edCodart.Text, NTSCInt(edFase.Text), NTSCInt(edMagaz.Text), _
                                         dQtaTot, DateTime.Now.ToShortDateString, dttLotti) Then Return
      dTmp = dQtaTot
      For Each dtrT As DataRow In dttLotti.Rows
        'ho i lotti da scaricare ordinati per priorit‡:
        'rintraccio lo stesso record nella griglia dei lotti aperti e calcolo la Qta
        Dim dtrT1() As DataRow = dsHllo.Tables("LOTCPRO").Select("xx_lotto = " & dtrT!alo_lotto.ToString)
        If dtrT1.Length > 0 Then
          If NTSCDec(dtrT1(0)!xx_dispnetta) >= dTmp Then
            dtrT1(0)!xx_qtaeva = dTmp
            dTmp = 0
          Else
            dtrT1(0)!xx_qtaeva = NTSCDec(dtrT1(0)!xx_dispnetta)
            dTmp -= NTSCDec(dtrT1(0)!xx_dispnetta)
          End If
        End If
        If dTmp = 0 Then Exit For
      Next
      dttLotti.Clear()
      dsHllo.Tables("LOTCPRO").AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grHllo_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grHllo.MouseDoubleClick
    Try
      If grvHllo.NTSGetCurrentDataRow() Is Nothing Then Return
      cmdConferma_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class